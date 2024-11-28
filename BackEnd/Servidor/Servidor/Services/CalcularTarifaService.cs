using Microsoft.SqlServer.Server;
using Servidor.Interfaces;

namespace Servidor.Services
{
    public class CalcularTarifaService : ITarifa
    {
        public decimal calcTarifa(int vehiculoId, string formato, int cantidad) //cantidad de horas o minutos, dependiendo el segundo parametro
        {

            // Define tarifas base por hora para diferentes tipos de vehículos
            Dictionary<int, decimal> tarifasPorHora = new Dictionary<int, decimal>
            {
                { 1, 10.00m }, // tipo de vehículo 1 tiene tarifa de 10.00 por hora
                { 2, 20.00m }, // tipo de vehículo 2 tiene tarifa de 15.00 por hora
                { 3, 30.00m }  // tipo de vehículo 3 tiene tarifade 20.00 por hora
            };

            // Validar si el tipo de vehículo es válido
            if (!tarifasPorHora.ContainsKey(vehiculoId))
            {
                throw new ArgumentException("Tipo de vehículo no válido.");
            }

            // Obtener la tarifa base por hora
            decimal tarifaPorHora = tarifasPorHora[vehiculoId];

            // Calcular la tarifa total dependiendo del formato (horas o minutos)
            decimal tarifaTotal = 0;

            if (formato.ToLower() == "horas")
            {
                tarifaTotal = tarifaPorHora * cantidad; // Multiplicar por cantidad de horas
            }
            else if (formato.ToLower() == "minutos")
            {
                if (cantidad <= 15)
                {
                    tarifaTotal = 0; // Si los minutos son menores a 15, la tarifa es 0
                }
                else
                {
                    // Calcular horas completas considerando los primeros 15 minutos gratis por cada hora
                    int horasCompletas = (cantidad - 15) / 60;
                    if ((cantidad - 15) % 60 > 0) horasCompletas++; // Redondear hacia arriba si hay minutos restantes

                    tarifaTotal = tarifaPorHora * horasCompletas;
                }
            }
            else
            {
                throw new ArgumentException("Formato no valido. Use 'horas' o 'minutos'.");
            }

            // Retornar la tarifa calculada
            return tarifaTotal;
        }

        public decimal GetMonto(DateTime horaEntrada, int vehiculoId)
        {
            // Obtener la hora actual (hora de salida)
            DateTime horaSalida = DateTime.Now;

            // Calcular la diferencia en minutos entre la hora de entrada y la hora de salida
            int diferenciaMinutos = (int)(horaSalida - horaEntrada).TotalMinutes;

            // Diccionario de tarifas por hora
            Dictionary<int, decimal> tarifasPorHora = new Dictionary<int, decimal>
            {
                { 1, 10.00m }, // tipo de vehículo 1 tiene tarifa de 10.00 por hora
                { 2, 20.00m }, // tipo de vehículo 2 tiene tarifa de 20.00 por hora
                { 3, 30.00m }  // tipo de vehículo 3 tiene tarifa de 30.00 por hora
            };

            // Validar si el tipo de vehículo es válido
            if (!tarifasPorHora.ContainsKey(vehiculoId))
            {
                throw new ArgumentException("Tipo de vehículo no válido.");
            }

            // Obtener la tarifa base por hora
            decimal tarifaPorHora = tarifasPorHora[vehiculoId];

            // Calcular la tarifa total
            decimal tarifaTotal = 0;

            // Si el tiempo transcurrido es menor o igual a 15 minutos, es gratis
            if (diferenciaMinutos <= 15)
            {
                tarifaTotal = 0;
            }
            else
            {
                // Si el tiempo transcurrido es mayor a 15 minutos, calcular el monto
                int minutosCobrables = diferenciaMinutos - 15;
                decimal horasCompletas = (decimal)minutosCobrables / 60;

                // Si el tiempo es menor a 1 hora, se cobra por 1 hora
                if (horasCompletas < 1)
                {
                    tarifaTotal = tarifaPorHora;
                }
                else
                {
                    // Redondear hacia arriba si hay fracción de hora
                    tarifaTotal = tarifaPorHora * Math.Ceiling(horasCompletas);
                }
            }

            // Retornar la tarifa calculada
            return tarifaTotal;
        }
    }
}
