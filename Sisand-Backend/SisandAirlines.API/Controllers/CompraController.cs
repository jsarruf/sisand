using Microsoft.AspNetCore.Mvc;
using SisandAirlines.Application.DTOs;
using SisandAirlines.Infrastructure.Repositories;

namespace SisandAirlines.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompraController : ControllerBase
    {
        private readonly CompraRepository _repository;

        public CompraController(CompraRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult FinalizarCompra([FromBody] CompraRequest  compra)
        {
            var id = _repository.SalvarCompra(compra);
            return Ok(new { sucesso = true, id });
        }
    }
}