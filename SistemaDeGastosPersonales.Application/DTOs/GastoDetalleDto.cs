namespace SistemaDeGastosPersonales.Application.DTOs
{
    public class GastoDetalleDto
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }   // ej: "Comida"
        public string MetodoPago { get; set; }  // ej: "Efectivo"
    }
}
