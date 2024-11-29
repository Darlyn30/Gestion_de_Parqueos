using Servidor.Models;

namespace Servidor.Interfaces
{
    public interface ISesion
    {
        //esto se encarga de controlar la sesion, que si el admin, no ha cerrado la sesion, no hay necesidad
        // de volver a iniciar sesion
        List<SesionIniciada> sesionIniciada();
        void deleteSesion(string email);
        void keepSesion(string email);
    }
}
