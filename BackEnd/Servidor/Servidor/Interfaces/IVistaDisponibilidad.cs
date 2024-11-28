using Servidor.Models;

namespace Servidor.Interfaces
{
    public interface IVistaDisponibilidad
    {
        List<VistaDisponibilidad> GetInfo();
        string SetEstacionamientos(string tipo);
    }
}
