using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeGastosPersonales.Domain.Entidades
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; } // aqui va el Hash, no la clave real

        // relaciones
        public virtual ICollection<Gasto> Gastos { get; set; }
        public virtual ICollection<Categoria> Categorias { get; set; }
        public virtual ICollection<MetodoPago> MetodosPago { get; set; }
    }
}
