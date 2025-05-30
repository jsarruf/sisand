using Microsoft.AspNetCore.Mvc;
using SisandAirlines.API.Models;
using SisandAirlines.Application.DTOs;
using SisandAirlines.Infrastructure.Repositories;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace SisandAirlines.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VooController : ControllerBase
    {
        private readonly CompraRepository _repo;

        public VooController(CompraRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("compra")]
        public async Task<IActionResult> PostCompra([FromBody] CompraRequest request)
        {
            var clienteId = await _repo.InserirClienteSeNaoExistir(request.Nome, request.Email, request.CPF, request.DataNascimento);
            var primeiroVoo = request.Voos.First();

            var compraId = await _repo.RegistrarCompra(clienteId, primeiroVoo.VooId, "PIX");

            var assentos = Enumerable.Range(1, primeiroVoo.Quantidade).ToList(); // simulação
            await _repo.InserirItensCompra(compraId, assentos);

            return Ok(new
            {
                mensagem = $"Compra registrada com sucesso para {request.Nome}",
                codigoConfirmacao = Guid.NewGuid().ToString("N")[..8].ToUpper()
            });
        }

        [HttpGet("disponiveis")]
        public async Task<IActionResult> GetVoos([FromQuery] string data, [FromQuery] int passageiros)
        {
            if (!DateTime.TryParse(data, out var dataConvertida))
                return BadRequest("Data inválida");

            var voos = await _repo.BuscarVoosDisponiveis(dataConvertida, passageiros);
            return Ok(voos);
        }

    }
}