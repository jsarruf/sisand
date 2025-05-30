using System;


namespace SisandAirlines.API.Models;

public class Voo
{
    public string Horario { get; set; }
    public int AssentosEconomicos { get; set; }
    public int AssentosPrimeiraClasse { get; set; }
    public double PrecoEconomico { get; set; }
    public double PrecoPrimeiraClasse { get; set; }
    public DateTime Data { get; set; }
}
