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

        

        public async Task ingresarVehiculo(int tipoVehiculo)
        {
            var tipoParam = new SqlParameter("@TipoVehiculoId", tipoVehiculo);

            // Llama al procedimiento almacenado con FromSqlRaw
            await ingreso_auto.FromSqlRaw("EXEC RegistrarEntrada @TipoVehiculoId", tipoParam).ToListAsync();
        }

        public async Task BorrarVehiculo(string Code, int TipoVehiculoId)
        {
            try
            {
                var tipoParam = new SqlParameter("@TipoVehiculoId", TipoVehiculoId);
                var codigoParam = new SqlParameter("@Code", Code);

                await ingreso_auto.FromSqlRaw("EXEC RegistrarSalida @Code = @Code, @TipoVehiculoId = @TipoVehiculoId", codigoParam, tipoParam).ToListAsync();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // Puedes loguear el error o retornar un mensaje de error al cliente
                throw new Exception("Error al ejecutar el procedimiento almacenado", ex);
            }
        }

        public DbSet<TipoAuto> tipo_vehiculos {  get; set; }

        public DbSet<Estacionamientos> Estacionamientos { get; set; }
        public DbSet<LogsMessages> LogMessages { get; set; }
    }
}
