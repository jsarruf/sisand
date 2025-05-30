using System;

namespace SisandAirlines.Application.DTOs
{
    public class RegisterDTO
    {
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
    }
}