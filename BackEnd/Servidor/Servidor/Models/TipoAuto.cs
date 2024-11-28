using System.ComponentModel.DataAnnotations;

namespace Servidor.Models
{
    public class TipoAuto
    {
        [Key]
        public int Id { get; set; }
        public string Tipo {  get; set; }
        
    }
}
