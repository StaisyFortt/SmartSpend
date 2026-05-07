using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SistemaDeGastosPersonales.Application.Interfaces;
using SistemaDeGastosPersonales.Application.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SistemaDeGastosPersonales.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriasController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CategoriaDto dto)
        {
            try
            {
                // sacar el id del token
                var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                await _service.CrearCategoriaAsync(dto, idUsuario);
                return Ok(new { mensaje = "Categoría creada correctamente." });
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
            var lista = await _service.ObtenerCategoriasPorUsuarioAsync(idUsuario);
            return Ok(lista);
        }
    }
}
