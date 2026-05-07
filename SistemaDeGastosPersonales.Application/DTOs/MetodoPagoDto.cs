using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGastosPersonales.Application.DTOs
{
    public class MetodoPagoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } // ej: "tarjeta debito"
        public string Icono { get; set; }  // ej: "credit-card" (texto para el frontend)
    }
}
