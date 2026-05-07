using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGastosPersonales.Application.DTOs
{
    public class CategoriaDto
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } // no poner usuarioId aqui, es secreto del sistema
    }
}
