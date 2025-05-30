using Microsoft.AspNetCore.Mvc;
using SisandAirlines.Application.DTOs;

namespace SisandAirlines.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO dto)
        {
            // Simular sucesso
            return Ok(new { message = "Usuário registrado com sucesso" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            // Simular token JWT
            return Ok(new { token = "fake-jwt-token" });
        }
    }
}