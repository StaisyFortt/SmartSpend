using System.ComponentModel.DataAnnotations;

namespace SistemaDeGastosPersonales.Application.DTOs
{
    public class GastoCreacionDto
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Monto { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public string Descripcion { get; set; }

        [Required]
        public int CategoriaId { get; set; } 

        [Required]
        public int MetodoPagoId { get; set; } 
    }
}
