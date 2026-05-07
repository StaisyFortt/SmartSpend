using SistemaDeGastosPersonales.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace SistemaDeGastosPersonales.Application.Interfaces
{
    public interface IGastoService 
    {
        Task RegistrarGastoAsync(GastoCreacionDto dto, int usuarioId);
        Task<IEnumerable<GastoDetalleDto>> ObtenerGastosAsync(int usuarioId, DateTime? fechaInicio, DateTime? fechaFin, string? busqueda);
        Task<string> ImportarGastosDesdeCsvAsync(IFormFile archivo, int usuarioId);
    }
}
