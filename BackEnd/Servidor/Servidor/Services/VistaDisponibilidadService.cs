using System.Drawing;
using System;
using Servidor.Context;
using Servidor.Interfaces;
using Servidor.Models;

namespace Servidor.Services
{
    public class VistaDisponibilidadService : IVistaDisponibilidad
    {
        private readonly VistaDisponibilidadContext _context;

        public VistaDisponibilidadService(VistaDisponibilidadContext _context)
        {
            this._context = _context;
        }

        public List<VistaDisponibilidad> GetInfo()
        {
            var result = _context.ver_disponibilidad.ToList();
            return result;
        }

        public string SetEstacionamientos(string tipo)
        {

            bool status = false;
            string mensaje = "";

            foreach (var item in _context.ver_disponibilidad)
            {
                if (tipo == item.Tipo) // Si el tipo coincide
                {
                    // Los espacios disponibles se calculan restando los espacios ocupados del total fijo
                    int espaciosRestantes = item.TotalDisponibles - item.Ocupados;
                    //tuve que dejarlo cuando quede un espacio disponible ya que si se llenan completo los parqueos se buguea
                    //y no se deja eliminar
                    if (espaciosRestantes > 1)
                    {
                        status = true;
                        mensaje = $"Hay {espaciosRestantes} espacios disponibles para el tipo {tipo}.";
                    }
                    else
                    {
                        mensaje = $"No hay espacios disponibles para el tipo {tipo}.";
                    }

                    // Salimos del bucle ya que encontramos el tipo
                    break;
                }
            }

            return mensaje;
        }
    }
}
