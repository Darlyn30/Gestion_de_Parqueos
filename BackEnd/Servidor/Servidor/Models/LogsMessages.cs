using System.ComponentModel.DataAnnotations;

namespace Servidor.Models
{
    public class LogsMessages
    {
        [Key]
        public int Id { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaMensaje { get; set; }

    }
}
