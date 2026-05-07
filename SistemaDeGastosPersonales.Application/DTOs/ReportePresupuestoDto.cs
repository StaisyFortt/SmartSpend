namespace SistemaDeGastosPersonales.Application.DTOs
{
    public class ReportePresupuestoDto
    {
        public string Categoria { get; set; }
        public decimal LimitePresupuesto { get; set; }
        public decimal TotalGastado { get; set; }
        public decimal PorcentajeConsumido { get; set; }
        public string MensajeAlerta { get; set; } // ej: "cuidado! Vas al 80%"
        public string ColorAlerta { get; set; }   // ej: verde", "amarillo", "rojo"
    }
}
