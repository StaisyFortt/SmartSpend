using SistemaDeGastosPersonales.Application.Interfaces;
using SistemaDeGastosPersonales.Application.DTOs;
using SistemaDeGastosPersonales.Domain.Entidades;

namespace SistemaDeGastosPersonales.Application.Services
{
    public class MetodoPagoService : IMetodoPagoService
    {
        private readonly IGenericRepository<MetodoPago> _repositorio;

        public MetodoPagoService(IGenericRepository<MetodoPago> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task CrearMetodoAsync(MetodoPagoDto dto, int usuarioId)
        {
            // no permitir nombres duplicados para el MISMO usuario
            var todos = await _repositorio.GetAllAsync();
            if (todos.Any(m => m.UsuarioId == usuarioId && m.Nombre == dto.Nombre))
            {
                throw new Exception("Ya existe un método de pago con ese nombre.");
            }

            var metodo = new MetodoPago
            {
                Nombre = dto.Nombre,
                Icono = dto.Icono,
                UsuarioId = usuarioId
            };

            await _repositorio.AddAsync(metodo);
        }

        public async Task<IEnumerable<MetodoPagoDto>> ObtenerMetodosPorUsuarioAsync(int usuarioId)
        {
            var todos = await _repositorio.GetAllAsync();
            return todos
                .Where(m => m.UsuarioId == usuarioId)
                .Select(m => new MetodoPagoDto
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    Icono = m.Icono
                });
        }
    }
}
