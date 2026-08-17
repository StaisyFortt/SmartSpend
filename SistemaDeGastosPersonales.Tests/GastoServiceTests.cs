using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using SistemaDeGastosPersonales.Application.Interfaces;
using SistemaDeGastosPersonales.Application.DTOs;
using SistemaDeGastosPersonales.Application.Services;
using SistemaDeGastosPersonales.Domain.Entidades;

namespace SistemaDeGastosPersonales.Tests
{
    public class GastoServiceTests
    {
        private readonly Mock<IGenericRepository<Gasto>> _gastoRepoMock;
        private readonly Mock<IGenericRepository<Categoria>> _categoriaRepoMock;
        private readonly Mock<IGenericRepository<MetodoPago>> _metodoRepoMock;
        private readonly GastoService _service;

        public GastoServiceTests()
        {
            _gastoRepoMock = new Mock<IGenericRepository<Gasto>>();
            _categoriaRepoMock = new Mock<IGenericRepository<Categoria>>();
            _metodoRepoMock = new Mock<IGenericRepository<MetodoPago>>();

            _service = new GastoService(
                _gastoRepoMock.Object,
                _categoriaRepoMock.Object,
                _metodoRepoMock.Object
            );
        }

        [Fact]
        public async Task RegistrarGastoAsync_ConCategoriaInexistente_LanzaExcepcion()
        {
            int usuarioId = 1;
            var dto = new GastoCreacionDto
            {
                Monto = 100,
                Fecha = DateTime.Now,
                Descripcion = "Gasto de prueba",
                CategoriaId = 99,
                MetodoPagoId = 1
            };

            _categoriaRepoMock.Setup(repo => repo.GetByIdAsync(dto.CategoriaId))
                .ReturnsAsync((Categoria)null);

            var exception = await Assert.ThrowsAsync<Exception>(() =>
                _service.RegistrarGastoAsync(dto, usuarioId)
            );

            Assert.Equal("La categoría no existe o no te pertenece.", exception.Message);
            _gastoRepoMock.Verify(repo => repo.AddAsync(It.IsAny<Gasto>()), Times.Never);
        }

        [Fact]
        public async Task RegistrarGastoAsync_ConMetodoPagoInexistente_LanzaExcepcion()
        {
            int usuarioId = 1;
            var dto = new GastoCreacionDto
            {
                Monto = 100,
                Fecha = DateTime.Now,
                Descripcion = "Gasto de prueba",
                CategoriaId = 5,
                MetodoPagoId = 99
            };

            var categoriaValida = new Categoria { Id = 5, Nombre = "Comida", UsuarioId = usuarioId };

            _categoriaRepoMock.Setup(repo => repo.GetByIdAsync(dto.CategoriaId))
                .ReturnsAsync(categoriaValida);

            _metodoRepoMock.Setup(repo => repo.GetByIdAsync(dto.MetodoPagoId))
                .ReturnsAsync((MetodoPago)null);

            var exception = await Assert.ThrowsAsync<Exception>(() =>
                _service.RegistrarGastoAsync(dto, usuarioId)
            );

            Assert.Equal("El método de pago no existe o no te pertenece.", exception.Message);
            _gastoRepoMock.Verify(repo => repo.AddAsync(It.IsAny<Gasto>()), Times.Never);
        }

        [Fact]
        public async Task RegistrarGastoAsync_GastoValido_SeRegistraExitosamente()
        {
            int usuarioId = 1;
            var dto = new GastoCreacionDto
            {
                Monto = 150,
                Fecha = DateTime.Now,
                Descripcion = "Gasto de prueba válido",
                CategoriaId = 2,
                MetodoPagoId = 3
            };

            var categoriaValida = new Categoria { Id = 2, Nombre = "Transporte", UsuarioId = usuarioId };
            var metodoValido = new MetodoPago { Id = 3, Nombre = "Efectivo", UsuarioId = usuarioId };

            _categoriaRepoMock.Setup(repo => repo.GetByIdAsync(dto.CategoriaId))
                .ReturnsAsync(categoriaValida);

            _metodoRepoMock.Setup(repo => repo.GetByIdAsync(dto.MetodoPagoId))
                .ReturnsAsync(metodoValido);

            await _service.RegistrarGastoAsync(dto, usuarioId);

            _gastoRepoMock.Verify(repo => repo.AddAsync(It.Is<Gasto>(g =>
                g.Monto == dto.Monto &&
                g.Fecha == dto.Fecha &&
                g.Descripcion == dto.Descripcion &&
                g.CategoriaId == dto.CategoriaId &&
                g.MetodoPagoId == dto.MetodoPagoId &&
                g.UsuarioId == usuarioId
            )), Times.Once);
        }
    }
}
