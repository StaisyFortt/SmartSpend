using Microsoft.AspNetCore.Mvc;
using SistemaDeGastosPersonales.Application.Interfaces;
using SistemaDeGastosPersonales.Application.DTOs;

namespace SistemaDeGastosPersonales.API.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        
        // inyeccion del servicio 
        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioRegistroDto dto)
        {
            try
            {
                // solo llamamos al servicio y si algo falla el servicio lanzara error
                await _usuarioService.RegistrarUsuarioAsync(dto);

                return Ok(new { mensaje = "Usuario registrado exitosamente." });
            }
            catch (Exception ex)
            {
                // manejar errores (por ejemplo, email ya existe)
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                string token = await _usuarioService.LoginAsync(dto);
                return Ok(new { token = token }); // devolvemos el token al usuario
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message }); 
            }
        }
    }
}
