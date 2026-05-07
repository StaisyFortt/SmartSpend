using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaDeGastosPersonales.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorregirNombreAnio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Año",
                table: "Presupuestos",
                newName: "Anio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Anio",
                table: "Presupuestos",
                newName: "Año");
        }
    }
}
