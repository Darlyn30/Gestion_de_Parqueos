using System.ComponentModel.DataAnnotations;

namespace Servidor.Models
{
    public class Cuenta
    {
        [Key]
        public string Correo {  get; set; }
        public string Clave {  get; set; }
    }
}
