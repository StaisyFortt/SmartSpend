using SistemaDeGastosPersonales.Application.Interfaces;
using SistemaDeGastosPersonales.Application.DTOs;
using SistemaDeGastosPersonales.Domain.Entidades;

namespace SistemaDeGastosPersonales.Application.Services
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly IGenericRepository<Presupuesto> _repositorio;
        private readonly IGenericRepository<Gasto> _repoGasto;
        public PresupuestoService(
        IGenericRepository<Presupuesto> repositorio,
        IGenericRepository<Gasto> repoGasto) // <--- Inyectamos aquí
        {
            _repositorio = repositorio;
            _repoGasto = repoGasto;
        }

        public async Task CrearOActualizarPresupuestoAsync(PresupuestoDto dto, int usuarioId)
        {
            // 1. buscamos si ya existe presupuesto para esa categoria en ese mes/año
            var todos = await _repositorio.GetAllAsync();
            var existente = todos.FirstOrDefault(p =>
                p.UsuarioId == usuarioId &&
                p.CategoriaId == dto.CategoriaId &&
                p.Anio == dto.Anio &&
                p.Mes == dto.Mes);

            if (existente != null)
            {
                existente.MontoMaximo = dto.MontoMaximo;
                await _repositorio.UpdateAsync(existente);
            }
            else
            {
                // crear (si es nuevo)
                var nuevo = new Presupuesto
                {
                    UsuarioId = usuarioId,
                    CategoriaId = dto.CategoriaId,
                    MontoMaximo = dto.MontoMaximo,
                    Anio = dto.Anio,
                    Mes = dto.Mes
                };
                await _repositorio.AddAsync(nuevo);
            }
        }

        public async Task<IEnumerable<PresupuestoDto>> ObtenerPresupuestosAsync(int usuarioId, int anio, int mes)
        {
            var todos = await _repositorio.GetAllAsync();
            return todos
                .Where(p => p.UsuarioId == usuarioId && p.Anio == anio && p.Mes == mes)
                .Select(p => new PresupuestoDto
                {
                    Id = p.Id,
                    CategoriaId = p.CategoriaId,
                    MontoMaximo = p.MontoMaximo,
                    Anio = p.Anio,
                    Mes = p.Mes
                });
        }

        public async Task<IEnumerable<ReportePresupuestoDto>> ObtenerReporteMensualAsync(int usuarioId, int año, int mes)
        {
            // 1. obtener todos los presupuestos de ese mes
            var presupuestos = await _repositorio.GetAllAsync();
            var misPresupuestos = presupuestos
                .Where(p => p.UsuarioId == usuarioId && p.Anio == año && p.Mes == mes)
                .ToList();

            // 2. obtener todos los gastos de ese mes
            var gastos = await _repoGasto.GetAllAsync();
            var misGastos = gastos
                .Where(g => g.UsuarioId == usuarioId && g.Fecha.Year == año && g.Fecha.Month == mes)
                .ToList();

            var reporte = new List<ReportePresupuestoDto>();

            foreach (var pre in misPresupuestos)
            {
                // 3. sumar cuánto he gastado en ESTA categoria
                var gastadoEnCategoria = misGastos
                    .Where(g => g.CategoriaId == pre.CategoriaId)
                    .Sum(g => g.Monto);

                // 4. calcular porcentaje
                decimal porcentaje = 0;
                if (pre.MontoMaximo > 0)
                {
                    porcentaje = (gastadoEnCategoria / pre.MontoMaximo) * 100;
                }

                // 5. definir la alerta (logica del semaforo)
                string mensaje = "Normal";
                string color = "Verde";

                if (porcentaje >= 100)
                {
                    mensaje = "¡PRESUPUESTO EXCEDIDO!";
                    color = "Rojo";
                }
                else if (porcentaje >= 80)
                {
                    mensaje = "Peligro: Estás cerca del límite";
                    color = "Naranja";
                }
                else if (porcentaje >= 50)
                {
                    mensaje = "Atención: Vas a la mitad";
                    color = "Amarillo";
                }

                // 6. agregar a la lista final
                // (usamos ?.Nombre por si Lazy Loading falla mostrar algo generico)
                string nombreCategoria = pre.Categoria != null ? pre.Categoria.Nombre : "Categoria " + pre.CategoriaId;

                reporte.Add(new ReportePresupuestoDto
                {
                    Categoria = nombreCategoria,
                    LimitePresupuesto = pre.MontoMaximo,
                    TotalGastado = gastadoEnCategoria,
                    PorcentajeConsumido = Math.Round(porcentaje, 2), // redondear a 2 decimales
                    MensajeAlerta = mensaje,
                    ColorAlerta = color
                });
            }

            return reporte;
        }
    }

}
