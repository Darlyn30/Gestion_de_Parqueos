namespace Servidor.Models
{
    public class VistaDisponibilidad
    {
        // debemos agregar los componentes de las vistas como si fueran una tabla comun, pero sin PK
        public string Tipo { get; set; }
        public int TotalDisponibles { get; set; }
        public int Ocupados {  get; set; }
        public decimal Precio {  get; set; }

    }
}
