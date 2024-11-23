using Microsoft.EntityFrameworkCore;
using Servidor.Context;
using Servidor.Interfaces;
using Servidor.Models;

namespace Servidor.Services
{
    public class AdminService : IAdmin
    {
        CuentaContext _context;
       
        public AdminService(CuentaContext _context)
        {
            this._context = _context;
        }

        public List<Cuenta> GetCuentas()
        {
            var result = _context.cuentas.ToList();
            return result;
        }

        public void Set(Cuenta model)
        {
            _context.Add(model);
            _context.SaveChanges();
        }
    }
}
