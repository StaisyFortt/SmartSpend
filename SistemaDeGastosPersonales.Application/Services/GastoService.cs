using SistemaDeGastosPersonales.Application.Interfaces;
using SistemaDeGastosPersonales.Application.DTOs;
using SistemaDeGastosPersonales.Domain.Entidades;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace SistemaDeGastosPersonales.Application.Services
{
    public class GastoService : IGastoService
    {
        private readonly IGenericRepository<Gasto> _gastoRepository;
        private readonly IGenericRepository<Categoria> _categoriaRepository;
        private readonly IGenericRepository<MetodoPago> _metodoRepository;

        public GastoService(
            IGenericRepository<Gasto> gastoRepository,
            IGenericRepository<Categoria> categoriaRepository,
            IGenericRepository<MetodoPago> metodoRepository)
        {
            _gastoRepository = gastoRepository;
            _categoriaRepository = categoriaRepository;
            _metodoRepository = metodoRepository;
        }

        public async Task RegistrarGastoAsync(GastoCreacionDto dto, int usuarioId)
        {
            // 1. validar que la categoria existe Y pertenece al usuario
            var categoria = await _categoriaRepository.GetByIdAsync(dto.CategoriaId);
            if (categoria == null || categoria.UsuarioId != usuarioId)
            {
                throw new Exception("La categoría no existe o no te pertenece.");
            }

            // 2. validar metodo de pago
            var metodo = await _metodoRepository.GetByIdAsync(dto.MetodoPagoId);
            if (metodo == null || metodo.UsuarioId != usuarioId)
            {
                throw new Exception("El método de pago no existe o no te pertenece.");
            }

            // 3. crear el Gasto
            var gasto = new Gasto
            {
                Monto = dto.Monto,
                Fecha = dto.Fecha,
                Descripcion = dto.Descripcion,
                CategoriaId = dto.CategoriaId,
                MetodoPagoId = dto.MetodoPagoId,
                UsuarioId = usuarioId // vinculacion automatica
            };

            await _gastoRepository.AddAsync(gasto);
        }

        public async Task<IEnumerable<GastoDetalleDto>> ObtenerGastosAsync(int usuarioId, DateTime? fechaInicio, DateTime? fechaFin, string? busqueda)
        {
            // 1. traer todos los gastos
            var gastos = await _gastoRepository.GetAllAsync();

            // 2. empezamos filtrando SOLO por usuario
            var consulta = gastos.Where(g => g.UsuarioId == usuarioId);

            // 3. filtro de Fechas (si el usuario envio fechas)
            if (fechaInicio.HasValue)
            {
                consulta = consulta.Where(g => g.Fecha >= fechaInicio.Value);
            }
            if (fechaFin.HasValue)
            {
                consulta = consulta.Where(g => g.Fecha <= fechaFin.Value);
            }

            // 4. filtro de Texto (busca en descripcion o nombre de categoria)
            if (!string.IsNullOrEmpty(busqueda))
            {
                // pasamos todo a minusculas 
                consulta = consulta.Where(g =>
                    (g.Descripcion != null && g.Descripcion.ToLower().Contains(busqueda.ToLower())) ||
                    (g.Categoria != null && g.Categoria.Nombre.ToLower().Contains(busqueda.ToLower()))
                );
            }

            // 5. proyección final (Convertir a DTO)
            var listaDto = consulta
                .Select(g => new GastoDetalleDto
                {
                    Id = g.Id,
                    Monto = g.Monto,
                    Fecha = g.Fecha,
                    Descripcion = g.Descripcion,
                    Categoria = g.Categoria?.Nombre ?? "Sin Categoría",
                    MetodoPago = g.MetodoPago?.Nombre ?? "Sin Método"
                })
                .OrderByDescending(g => g.Fecha)
                .ToList();

            return listaDto;
        }

        public async Task<string> ImportarGastosDesdeCsvAsync(IFormFile archivo, int usuarioId)
        {
            var errores = new List<string>();
            int guardados = 0;

            // traemos todas las categorias y métodos del usuario a memoria para buscar rapido
            var categorias = (await _categoriaRepository.GetAllAsync()).Where(c => c.UsuarioId == usuarioId).ToList();
            var metodos = (await _metodoRepository.GetAllAsync()).Where(m => m.UsuarioId == usuarioId).ToList();

            using (var reader = new StreamReader(archivo.OpenReadStream()))
            {
                // leer la primera linea (Encabezados) y saltarla
                await reader.ReadLineAsync();

                int numeroLinea = 1;
                while (reader.Peek() >= 0)
                {
                    var linea = await reader.ReadLineAsync();
                    numeroLinea++;

                    // separar por comas
                    var datos = linea.Split(',');

                    // validar formato basico (debe tener 5 columnas)
                    if (datos.Length < 5)
                    {
                        errores.Add($"Línea {numeroLinea}: Formato incompleto.");
                        continue;
                    }

                    try
                    {
                        // 1. parsear datos
                        var fecha = DateTime.Parse(datos[0]); // columna 0: fecha
                        var descripcion = datos[1];           // columna 1: descripcion
                        var monto = decimal.Parse(datos[2], System.Globalization.CultureInfo.InvariantCulture); // Columna 2: Monto
                        var nombreCat = datos[3].Trim();      // columna 3: categoria
                        var nombreMet = datos[4].Trim();      // columna 4: metodo

                        // 2. buscar ID de Categoria o crearla si no existe
                        var categoria = categorias.FirstOrDefault(c => c.Nombre.Equals(nombreCat, StringComparison.OrdinalIgnoreCase));
                        if (categoria == null)
                        {
                            categoria = new Categoria { Nombre = nombreCat, UsuarioId = usuarioId, EsActiva = true };
                            await _categoriaRepository.AddAsync(categoria);
                            categorias.Add(categoria); // actualizar lista en memoria
                        }

                        // 3. buscar ID de metodo o crearlo si no existe
                        var metodo = metodos.FirstOrDefault(m => m.Nombre.Equals(nombreMet, StringComparison.OrdinalIgnoreCase));
                        if (metodo == null)
                        {
                            metodo = new MetodoPago { Nombre = nombreMet, UsuarioId = usuarioId, Icono = "fas fa-file-import" };
                            await _metodoRepository.AddAsync(metodo);
                            metodos.Add(metodo);
                        }

                        // 4. guardar el Gasto
                        var gasto = new Gasto
                        {
                            UsuarioId = usuarioId,
                            Fecha = fecha,
                            Descripcion = descripcion,
                            Monto = monto,
                            CategoriaId = categoria.Id,
                            MetodoPagoId = metodo.Id
                        };
                        await _gastoRepository.AddAsync(gasto);
                        guardados++;
                    }
                    catch (Exception)
                    {
                        errores.Add($"Línea {numeroLinea}: Datos inválidos (revise fechas o números).");
                    }
                }
            }

            return $"Proceso finalizado. Registros guardados: {guardados}. Errores: {errores.Count} \n" + string.Join("\n", errores);
        }
    }
}
