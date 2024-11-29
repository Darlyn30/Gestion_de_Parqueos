using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Servidor.Models;

namespace Servidor.Context
{
    public class SesionIniciadaContext : DbContext
    {
        public SesionIniciadaContext(DbContextOptions<SesionIniciadaContext> options)
            :base(options)
        {
        }

        public DbSet<SesionIniciada> SesionIniciada { get; set; }

        public void keepSesion(string email)
        {
            var tipoParam = new SqlParameter("@Email", email);

            // Llama al procedimiento almacenado con FromSqlRaw
            Database.ExecuteSqlRaw("EXEC sp_ConsultaEInsertaEnSesion @Email", tipoParam);
        }
    }
}
