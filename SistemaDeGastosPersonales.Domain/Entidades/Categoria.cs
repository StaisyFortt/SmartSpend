using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeGastosPersonales.Domain.Entidades
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        public bool EsActiva { get; set; } = true;

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        // relacion con gastos y presupuestos
        public virtual ICollection<Gasto> Gastos { get; set; }
        public virtual ICollection<Presupuesto> Presupuestos { get; set; }
    }
}
