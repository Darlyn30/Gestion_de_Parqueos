using System.ComponentModel.DataAnnotations;

namespace Servidor.Models
{
    public class Cuenta
    {
        //esta entidad es para la cuenta del administrador
        [Key]
        public string Correo {  get; set; }
        public string Clave {  get; set; }
    }
}
