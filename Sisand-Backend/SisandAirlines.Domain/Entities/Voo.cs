using System;
using System.Collections.Generic;


namespace SisandAirlines.Domain.Entities;

public class Voo
{
    public Guid Id { get; set; }
    public DateTime DataHoraPartida { get; set; }
    public DateTime DataHoraChegada => DataHoraPartida.AddHours(1);
    public int AssentosEconomicosDisponiveis { get; set; } = 5;
    public int AssentosPrimeiraClasseDisponiveis { get; set; } = 2;
    public decimal PrecoEconomico { get; set; } = 159.97m;
    public decimal PrecoPrimeiraClasse { get; set; } = 399.93m;
}