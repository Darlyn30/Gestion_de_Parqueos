using Servidor.Context;
using Servidor.Interfaces;
using Servidor.Models;

namespace Servidor.Services
{
    public class VistaDisponibilidadService : IVistaDisponibilidad
    {
        private readonly VistaDisponibilidadContext _context;

        public VistaDisponibilidadService(VistaDisponibilidadContext _context)
        {
            this._context = _context;
        }

        public List<VistaDisponibilidad> GetInfo()
        {
            var result = _context.ver_disponibilidad.ToList();
            return result;
        }
    }
}
