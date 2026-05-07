using SistemaDeGastosPersonales.Application.DTOs;
using SistemaDeGastosPersonales.Application.Interfaces;
using SistemaDeGastosPersonales.Domain.Entidades;

namespace SistemaDeGastosPersonales.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IGenericRepository<Categoria> _repositorio;

        public CategoriaService(IGenericRepository<Categoria> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task CrearCategoriaAsync(CategoriaDto dto, int usuarioId)
        {
            var todas = await _repositorio.GetAllAsync();
            if (todas.Any(c => c.UsuarioId == usuarioId && c.Nombre == dto.Nombre))
            {
                throw new Exception("Ya tienes una categoría con este nombre.");
            }

            var categoria = new Categoria
            {
                Nombre = dto.Nombre,
                UsuarioId = usuarioId, 
                EsActiva = true
            };

            await _repositorio.AddAsync(categoria);
        }

        public async Task<IEnumerable<CategoriaDto>> ObtenerCategoriasPorUsuarioAsync(int usuarioId)
        {
            var todas = await _repositorio.GetAllAsync();
            return todas
                .Where(c => c.UsuarioId == usuarioId && c.EsActiva)
                .Select(c => new CategoriaDto { Id = c.Id, Nombre = c.Nombre });
        }
    }
}