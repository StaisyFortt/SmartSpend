using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeGastosPersonales.Domain.Entidades
{
    public class MetodoPago
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)] 
        public string Nombre { get; set; }

        public string Icono { get; set; }

        // clave foranea al usuario
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<Gasto> Gastos { get; set; }
    }
}
