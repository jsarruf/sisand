using System;
using System.Collections.Generic;

namespace SisandAirlines.Application.DTOs
{
    public class CompraDTO
    {
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public string CPFCliente { get; set; }
        public DateTime DataNascimento { get; set; }
        public string FormaPagamento { get; set; } = "Pix";
        public List<CompraVooDTO> Voos { get; set; } = new();
    }

    public class CompraVooDTO
    {
        public int VooId { get; set; }
        public int QuantidadeEconomica { get; set; }
        public int QuantidadePrimeiraClasse { get; set; }
    }
}