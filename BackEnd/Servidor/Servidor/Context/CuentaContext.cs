using Microsoft.EntityFrameworkCore;
using Servidor.Models;

namespace Servidor.Context
{
    public class CuentaContext : DbContext
    {
        public CuentaContext(DbContextOptions<CuentaContext> options)
            : base(options)
        {
        }

        public DbSet<Cuenta> cuentas {  get; set; }
    }
}
