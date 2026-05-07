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
    public class PresupuestosController : ControllerBase
    {
        private readonly IPresupuestoService _service;
        
        public PresupuestosController(IPresupuestoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] PresupuestoDto dto)
        {
            try
            {
                var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _service.CrearOActualizarPresupuestoAsync(dto, idUsuario);
                return Ok(new { mensaje = "Presupuesto guardado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] int año, [FromQuery] int mes)
        {
            var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var lista = await _service.ObtenerPresupuestosAsync(idUsuario, año, mes);
            return Ok(lista);
        }

        [HttpGet("reporte")] // la ruta sera /api/Presupuestos/reporte
        public async Task<IActionResult> VerReporte([FromQuery] int anio, [FromQuery] int mes)
        {
            try
            {
                var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var resultado = await _service.ObtenerReporteMensualAsync(idUsuario, anio, mes);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
