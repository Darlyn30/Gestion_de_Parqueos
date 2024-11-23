using Microsoft.EntityFrameworkCore;
using Servidor.Models;

namespace Servidor.Context
{
    public class RegistroVistaAdminContext : DbContext
    {
        public RegistroVistaAdminContext(DbContextOptions<RegistroVistaAdminContext> options)
            :base(options)
        {
        }

        // solo tendra un GET por ahora el controlador de este
        public DbSet<RegistroVistaAdmin> RegistroVistaAdmin {  get; set; }
    }
}
