using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeGastosPersonales.Domain.Entidades
{
    public class Presupuesto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Anio { get; set; }

        [Required]
        [Range(1, 12)]
        public int Mes { get; set; }

        [Required]
        public decimal MontoMaximo { get; set; } // el limite de dinero

        // relacion: un presupuesto pertenece a una categoria especifica
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }

        // opcional... usuarioId para facilitar consultas
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
