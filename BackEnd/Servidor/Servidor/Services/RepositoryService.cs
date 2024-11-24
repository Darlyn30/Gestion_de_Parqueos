using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Servidor.Context;
using Servidor.Interfaces;
using Servidor.Models;

namespace Servidor.Services
{

    public class RepositoryService<T> : Irepository<T> where T : class
    {

        private readonly DbFinalProjectContext _context;
        DbSet<T> dbSet;

        public RepositoryService(DbFinalProjectContext _context)
        {
            this._context = _context;
            dbSet = _context.Set<T>();
            

        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public async Task<List<IngresoAuto>> SP_register(int id)
        {
            
            return await _context.ingresarVehiculo(id);
            
        }

        public async Task<List<IngresoAuto>> SP_eliminarRegister(string code, int id)
        {
            return await _context.BorrarVehiculo(id, code);
            
        }
    }
}
