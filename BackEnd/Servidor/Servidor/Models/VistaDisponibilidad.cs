namespace Servidor.Models
{
    public class VistaDisponibilidad
    {
        public string Tipo { get; set; }
        public int TotalDisponibles { get; set; }
        public int Ocupados {  get; set; }
        public decimal Precio {  get; set; }

    }
}
