using System;
using System.Collections.Generic;

namespace SisandAirlines.Application.DTOs
{
    public class CompraRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public List<VooSelecionado> Voos { get; set; }
    }

    public class VooSelecionado
    {
        public int VooId { get; set; }
        public string Horario { get; set; } = string.Empty;
        public string Classe { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public double Preco { get; set; }
        public DateTime Data { get; set; }
    }
}