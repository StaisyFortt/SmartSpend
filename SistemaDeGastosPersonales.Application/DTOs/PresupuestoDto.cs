using System.ComponentModel.DataAnnotations;

namespace SistemaDeGastosPersonales.Application.DTOs
{
    public class PresupuestoDto
    {
        public int Id { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [Required]
        public decimal MontoMaximo { get; set; } 

        [Required]
        [Range(2020,2100)]
        public int Anio { get; set; } 

        [Required]
        [Range(1,12)]
        public int Mes { get; set; }
    }
}
