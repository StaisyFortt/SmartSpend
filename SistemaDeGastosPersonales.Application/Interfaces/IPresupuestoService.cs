using SistemaDeGastosPersonales.Application.DTOs;

namespace SistemaDeGastosPersonales.Application.Interfaces
{
    public interface IPresupuestoService
    {
        Task CrearOActualizarPresupuestoAsync(PresupuestoDto dto, int usuarioId);
        Task<IEnumerable<PresupuestoDto>> ObtenerPresupuestosAsync(int usuarioId, int anio, int mes);
        Task<IEnumerable<ReportePresupuestoDto>> ObtenerReporteMensualAsync(int usuarioId, int anio, int mes);
    }
}
