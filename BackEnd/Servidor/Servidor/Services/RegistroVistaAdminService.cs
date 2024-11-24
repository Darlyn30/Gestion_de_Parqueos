using Servidor.Context;
using Servidor.Interfaces;
using Servidor.Models;

namespace Servidor.Services
{
    public class RegistroVistaAdminService : IVistaAdmin
    {
        RegistroVistaAdminContext _context;
        public RegistroVistaAdminService(RegistroVistaAdminContext _context)
        {
            this._context = _context;
        }
        public List<RegistroVistaAdmin> GetAllRegister()
        {
            var result = _context.RegistroVistaAdmin.ToList();
            return result;
        }

        
    }
}
