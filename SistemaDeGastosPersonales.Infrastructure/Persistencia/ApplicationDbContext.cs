using Microsoft.EntityFrameworkCore;
using SistemaDeGastosPersonales.Domain.Entidades;

namespace SistemaDeGastosPersonales.Infrastructure.Persistencia
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // convierte estas clases en tablas de la base de datos

        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Presupuesto> Presupuestos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configuracion de decimales (siempre necesaria para dinero)
            modelBuilder.Entity<Gasto>()
                .Property(g => g.Monto)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Presupuesto>()
                .Property(p => p.MontoMaximo)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.Usuario)
                .WithMany(u => u.Gastos)
                .HasForeignKey(g => g.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.Categoria)
                .WithMany(c => c.Gastos)
                .HasForeignKey(g => g.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.MetodoPago)
                .WithMany(m => m.Gastos)
                .HasForeignKey(g => g.MetodoPagoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Presupuesto>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
