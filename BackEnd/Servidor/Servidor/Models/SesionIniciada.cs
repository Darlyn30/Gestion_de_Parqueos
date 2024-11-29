using System.ComponentModel.DataAnnotations;

namespace Servidor.Models
{
    public class SesionIniciada
    {
        [Key]
        public string Correo { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
    }
}
