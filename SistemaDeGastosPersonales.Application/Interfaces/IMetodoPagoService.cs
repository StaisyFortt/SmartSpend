using SistemaDeGastosPersonales.Application.DTOs;

namespace SistemaDeGastosPersonales.Application.Interfaces
{
    public interface IMetodoPagoService
    {
        Task CrearMetodoAsync(MetodoPagoDto dto, int usuarioId);
        Task<IEnumerable<MetodoPagoDto>> ObtenerMetodosPorUsuarioAsync(int usuarioId);
    }
}
