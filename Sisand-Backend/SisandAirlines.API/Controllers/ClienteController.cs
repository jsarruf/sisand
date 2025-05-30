using Microsoft.AspNetCore.Mvc;
using SisandAirlines.API.Models;
using SisandAirlines.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace SisandAirlines.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly CompraRepository _repo;

        public ClienteController(CompraRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var cliente = await _repo.BuscarClientePorEmailSenha(login.Email, login.Senha);
            if (cliente == null)
                return Unauthorized();

            return Ok(cliente);
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] ClienteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
                return BadRequest("Dados obrigatórios ausentes");

            var existente = await _repo.BuscarClientePorEmail(request.Email);
            if (existente != null)
                return Conflict("Cliente já cadastrado");

            var clienteId = await _repo.InserirCliente(request);
            var cliente = await _repo.BuscarClientePorId(clienteId);

            return Ok(cliente);
        }

    }
}
