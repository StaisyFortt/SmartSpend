using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeGastosPersonales.Domain.Entidades
{
    public class Gasto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Monto { get; set; } // debe ser positivo (validar en DTO/Servicio)

        [Required]
        public DateTime Fecha { get; set; }

        [MaxLength(200)]
        public string Descripcion { get; set; }

        // relaciones

        [Required]
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }

        [Required]
        public int MetodoPagoId { get; set; }
        [ForeignKey("MetodoPagoId")]
        public virtual MetodoPago MetodoPago { get; set; }

        [Required]
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
    }
}
