using SistemaDeGastosPersonales.Application.DTOs;

namespace SistemaDeGastosPersonales.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task RegistrarUsuarioAsync(UsuarioRegistroDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}
