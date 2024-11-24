using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Servidor.Models;

namespace Servidor.Context
{
    public class DbFinalProjectContext : DbContext
    {
        public DbFinalProjectContext(DbContextOptions<DbFinalProjectContext> options)
            : base(options)
        {
        }

        public DbSet<IngresoAuto> ingreso_auto { get; set; }

        

        public async Task<List<IngresoAuto>> ingresarVehiculo(int tipoVehiculo)
        {
            var tipoParam = new SqlParameter("@TipoVehiculoId", tipoVehiculo);

            // Llama al procedimiento almacenado con FromSqlRaw
            return await ingreso_auto.FromSqlRaw("EXEC RegistrarEntrada @TipoVehiculoId", tipoParam).ToListAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngresoAuto>()
                .ToTable(tb => tb.HasTrigger("asignarCodeAll"));
        }

        public async Task<List<IngresoAuto>> BorrarVehiculo(int tipoVehiculo, string code)
        {
            var tipoParam = new SqlParameter("@TipoVehiculoId", tipoVehiculo);
            var codigo = new SqlParameter("@Code", code);

            // Llama al procedimiento almacenado con FromSqlRaw
            return await ingreso_auto.FromSqlRaw("EXEC RegistrarSalida @Code", codigo, "@TipoVehiculoId", tipoParam).ToListAsync();
        }

        public DbSet<TipoAuto> tipo_vehiculos {  get; set; }

        public DbSet<Estacionamientos> Estacionamientos { get; set; }
        public DbSet<MontoPagar> montoPagarCar {  get; set; }
    }
}
