using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SistemaDeGastosPersonales.Application.DTOs;
using SistemaDeGastosPersonales.Application.Interfaces;
using SistemaDeGastosPersonales.Domain.Entidades;
using BCrypt.Net; // para el hash

namespace SistemaDeGastosPersonales.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _usuarioRepository;
        private readonly IConfiguration _config; // para leer appsettings

        public UsuarioService(IGenericRepository<Usuario> usuarioRepository, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _config = config; 
        }

        public async Task RegistrarUsuarioAsync(UsuarioRegistroDto dto)
        {
            // 1. convertir DETO a entidad
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                // 2. hashear la contraseña
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            // 3. guardar en la base de datos
            await _usuarioRepository.AddAsync(usuario);
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            // 1. buscar el usuario por Email
            var usuarios = await _usuarioRepository.GetAllAsync();
            var usuario = usuarios.FirstOrDefault(u => u.Email == dto.Email);

            // 2. validar si existe
            if (usuario == null)
            {
                throw new Exception("Credenciales inválidas"); 
            }

            // 3. verificar contraseña
            bool esPasswordValido = BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash);
            if (!esPasswordValido)
            {
                throw new Exception("Credenciales inválidas");
            }

            // 4. generar el Token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]); // leemos la clave del json

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()), // guardamos el ID del usuario
                new Claim(ClaimTypes.Email, usuario.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(2), // el token dura 2 horas
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); // devolvemos el string del token
        }
    }
}
