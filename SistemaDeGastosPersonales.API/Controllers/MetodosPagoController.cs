using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SistemaDeGastosPersonales.Application.Interfaces;
using SistemaDeGastosPersonales.Application.DTOs;
using System.Security.Claims;

namespace SistemaDeGastosPersonales.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MetodosPagoController : ControllerBase
    {
        private readonly IMetodoPagoService _service;
        
        public MetodosPagoController(IMetodoPagoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] MetodoPagoDto dto)
        {
            try
            {
                var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _service.CrearMetodoAsync(dto, idUsuario);
                return Ok(new { mensaje = "Método de pago creado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var lista = await _service.ObtenerMetodosPorUsuarioAsync(idUsuario);
            return Ok(lista);
        }
    }
}
