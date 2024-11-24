using System.ComponentModel.DataAnnotations;

namespace Servidor.Models
{
    public class RegistroVistaAdmin
    {
        [Key]
        public int Id { get; set; }
        public string Codigo {  get; set; }
        public int TipoVehiculoId {  get; set; }
        public int EstacionamientoId {  get; set; }
        public int montoPagarId {  get; set; }
    }
}
