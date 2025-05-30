using System;

namespace SisandAirlines.API.Models
{
    public class ClienteRequest
    {
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string DataNascimento { get; set; }
        public string Senha { get; set; }
    }
}