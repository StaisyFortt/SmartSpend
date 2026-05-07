using SistemaDeGastosPersonales.Application.DTOs;

namespace SistemaDeGastosPersonales.Application.Interfaces
{
    public interface ICategoriaService
    {
        Task CrearCategoriaAsync(CategoriaDto dto, int usuarioId);
        Task<IEnumerable<CategoriaDto>> ObtenerCategoriasPorUsuarioAsync(int usuarioId);
    }
}
