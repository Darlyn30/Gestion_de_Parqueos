using System.ComponentModel.DataAnnotations;

namespace Servidor.Models
{
    public class IngresoAuto
    {
        [Key]
        public string Codigo { get; set; }
        public DateTime HoraEntrada {  get; set; }
    }
}
