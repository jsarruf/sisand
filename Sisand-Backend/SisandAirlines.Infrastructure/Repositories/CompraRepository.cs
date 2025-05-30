using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Linq;
using SisandAirlines.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SisandAirlines.Infrastructure.Repositories
{
    public class CompraRepository
    {
        private readonly IDbConnection _db;

        public CompraRepository(IConfiguration config)
        {
            _db = new NpgsqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<int> InserirClienteSeNaoExistir(string nome, string email, string cpf, DateTime nascimento)
        {
            var clienteId = await _db.ExecuteScalarAsync<int?>(@"
                SELECT id FROM clientes WHERE cpf = @cpf", new { cpf });

            if (clienteId.HasValue)
                return clienteId.Value;

            return await _db.ExecuteScalarAsync<int>(@"
                INSERT INTO clientes (nome_completo, email, cpf, data_nascimento, senha_hash)
                VALUES (@nome, @email, @cpf, @nascimento, '')
                RETURNING id", new { nome, email, cpf, nascimento });
        }

        public async Task<int> RegistrarCompra(int clienteId, int vooId, string formaPagamento)
        {
            var codigo = Guid.NewGuid();
            return await _db.ExecuteScalarAsync<int>(@"
                INSERT INTO compras (cliente_id, voo_id, forma_pagamento, codigo_confirmacao)
                VALUES (@clienteId, @vooId, @formaPagamento, @codigo)
                RETURNING id", new { clienteId, vooId, formaPagamento, codigo });
        }

        public async Task InserirItensCompra(int compraId, List<int> assentosIds)
        {
            foreach (var id in assentosIds)
            {
                await _db.ExecuteAsync("INSERT INTO itens_compra (compra_id, assento_id) VALUES (@compraId, @assentoId)", new { compraId, assentoId = id });
                await _db.ExecuteAsync("UPDATE assentos SET ocupado = true WHERE id = @id", new { id });
            }
        }

        public async Task SalvarCompra(CompraRequest request)
        {
            var clienteId = await InserirClienteSeNaoExistir(
                request.Nome,
                request.Email,
                request.CPF,
                request.DataNascimento
            );

            var vooSelecionado = request.Voos.First();

            int compraId = await RegistrarCompra(clienteId, vooSelecionado.VooId, "PIX");

            var assentos = new List<int>();
            for (int i = 0; i < vooSelecionado.Quantidade; i++)
            {
                assentos.Add(0);
            }

            await InserirItensCompra(compraId, assentos);
        }

        public async Task<IEnumerable<object>> BuscarVoosDisponiveis(DateTime data, int passageiros)
        {
            var sql = @"
                SELECT 
                    v.id,
                    v.data_partida AS partida,
                    v.data_chegada AS chegada,
                    COUNT(CASE WHEN a.tipo = 'ECONOMICA' AND NOT a.ocupado THEN 1 END) AS assentosEconomicosDisponiveis,
                    COUNT(CASE WHEN a.tipo = 'PRIMEIRA' AND NOT a.ocupado THEN 1 END) AS assentosPrimeiraClasseDisponiveis,
                    159.97 AS precoEconomico,
                    399.93 AS precoPrimeira
                FROM voos v
                LEFT JOIN assentos a ON v.id = a.voo_id
                WHERE DATE(v.data_partida) = @data
                GROUP BY v.id, v.data_partida, v.data_chegada
                HAVING
                    COUNT(CASE WHEN a.tipo = 'ECONOMICA' AND NOT a.ocupado THEN 1 END) >= @passageiros
                    OR
                    COUNT(CASE WHEN a.tipo = 'PRIMEIRA' AND NOT a.ocupado THEN 1 END) >= @passageiros
                ORDER BY v.data_partida
            ";

            return await _db.QueryAsync(sql, new { data, passageiros });
        }
        
        public async Task<dynamic> BuscarClientePorEmailSenha(string email, string senha)
        {
            return await _db.QueryFirstOrDefaultAsync(@"
                SELECT id, nome_completo, email, cpf, data_nascimento
                FROM clientes
                WHERE email = @email AND senha_hash = @senha", new { email, senha });
        }

    }
}