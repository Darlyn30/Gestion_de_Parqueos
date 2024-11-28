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

        public void SP_register(int id)
        {
            _context.ingresarVehiculo(id);
            
        }

        public void SP_eliminarRegister(string code, int id)
        {
            _context.BorrarVehiculo(code, id);
            
        }

        public string saveLogTxt()
        {
            string path = "C:\\Users\\Admin\\Videos\\ProyectosJS\\proyecto-final-prog2\\BackEnd\\Servidor\\Servidor\\Log.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    foreach (var item in _context.LogMessages)
                    {
                        sw.WriteLine($"{item.Id}: {item.Mensaje}\n");
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "Txt Enviado Correctamente";
        }


    }
}
