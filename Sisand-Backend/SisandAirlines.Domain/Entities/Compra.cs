using System;
using System.Collections.Generic;

namespace SisandAirlines.Domain.Entities;

public class Compra
{
    public Guid Id { get; set; }
    public string NomeCliente { get; set; } = string.Empty;
    public string EmailCliente { get; set; } = string.Empty;
    public string CPFCliente { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string FormaPagamento { get; set; } = "Pix";
    public DateTime DataCompra { get; set; } = DateTime.UtcNow;
    public List<CompraVoo> Voos { get; set; } = new();
}

public class CompraVoo
{
    public Guid VooId { get; set; }
    public int QuantidadeEconomica { get; set; }
    public int QuantidadePrimeiraClasse { get; set; }
}