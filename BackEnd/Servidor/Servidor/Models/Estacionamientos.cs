using System.ComponentModel.DataAnnotations;

namespace Servidor.Models
{
    public class Estacionamientos
    {
        [Key]
        public int Id { get; set; }
        public int TipoVehiculoId {  get; set; }
        public int TotalDisponibles {  get; set; }
        public int Ocupados {  get; set; }

    }
}
