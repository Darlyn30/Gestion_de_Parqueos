using Microsoft.EntityFrameworkCore;
using Servidor.Models;

namespace Servidor.Context
{
    public class VistaDisponibilidadContext : DbContext
    {
        public VistaDisponibilidadContext(DbContextOptions<VistaDisponibilidadContext> options)
            :base(options)
        {
        }

        //ESTO ESTA MAL, ESTO SE VA A ELIMINAR
        public DbSet<VistaDisponibilidad> ver_disponibilidad { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la vista
            modelBuilder.Entity<VistaDisponibilidad>(entity =>
            {
                entity.HasNoKey(); // Indicar que no hay clave primaria
                entity.ToView("ver_disponibilidad"); // Nombre exacto de la vista en SQL Server
            });
        }
    }
}
