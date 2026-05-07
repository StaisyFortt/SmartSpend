using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaDeGastosPersonales.Application.DTOs;
using SistemaDeGastosPersonales.Application.Interfaces;
using System.Security.Claims;
using System.Text;

namespace SistemaDeGastosPersonales.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GastosController : ControllerBase
    {
        private readonly IGastoService _service;

        public GastosController(IGastoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] GastoCreacionDto dto)
        {
            try
            {
                var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await _service.RegistrarGastoAsync(dto, idUsuario);
                return Ok(new { mensaje = "Gasto registrado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar(
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFin,
            [FromQuery] string? busqueda)
        {
            try
            {
                var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var lista = await _service.ObtenerGastosAsync(idUsuario, fechaInicio, fechaFin, busqueda);

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("exportar")] // ruta: /api/Gastos/exportar
        public async Task<IActionResult> ExportarGasto()
        {
            try
            {
                var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                // 1. reutilizar el servicio que ya creamos para traer los gastos 
                var gastos = await _service.ObtenerGastosAsync(idUsuario, null, null, null);

                // 2. convertir a CSV
                var builder = new StringBuilder();

                // encabezados
                builder.AppendLine("Id,Fecha,Categoria,MetodoPago,Descripcion,Monto");

                foreach (var g in gastos)
                {
                    builder.AppendLine($"{g.Id},{g.Fecha.ToShortDateString()},{g.Categoria},{g.MetodoPago},\"{g.Descripcion}\",{g.Monto}");
                }

                // 3. convertimos el texto a bytes
                var bytes = Encoding.UTF8.GetBytes(builder.ToString());

                // 4. devolver el archivo
                return File(bytes, "text/csv", "MisGastos.csv");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("importar")] // POST /api/Gastos/importar
        public async Task<IActionResult> Importar(IFormFile archivo)
        {
            try
            {
                if (archivo == null || archivo.Length == 0)
                    return BadRequest(new { error = "No se envió ningún archivo." });

                var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                var reporte = await _service.ImportarGastosDesdeCsvAsync(archivo, idUsuario);

                return Ok(new { mensaje = reporte });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
