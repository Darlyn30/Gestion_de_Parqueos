using System.ComponentModel.DataAnnotations;

namespace Servidor.Models
{
    public class MontoPagar
    {
        [Key]
        public int Id { get; set; }
        public string Codigo {  get; set; }
        public decimal Precio {  get; set; }
        public int IdEstacionamiento {  get; set; }

    }
}
