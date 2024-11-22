using Microsoft.EntityFrameworkCore;
using Servidor.Context;
using Servidor.Interfaces;

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

        public void Add(T item)
        {
            dbSet.Add(item);
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }
    }
}
