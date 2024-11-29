using Servidor.Context;
using Servidor.Interfaces;
using Servidor.Models;

namespace Servidor.Services
{
    public class SesionIniciadaService : ISesion
    {
        private readonly SesionIniciadaContext _context;
        public SesionIniciadaService(SesionIniciadaContext _context)
        {
            this._context = _context;
        }

        public List<SesionIniciada> sesionIniciada()
        {
            var result = _context.SesionIniciada.ToList();
            return result;
        }



        // se ejecuta cuando se cierra sesion en la pagina, y registra el correo que hay en la tabla de sesion
        public void deleteSesion(string email)
        {
            var registro = _context.SesionIniciada.Where(user => user.Correo == email).FirstOrDefault();
            _context.SesionIniciada.Remove(registro!);
            _context.SaveChanges();
        }

        public void keepSesion(string email)
        {
            _context.keepSesion(email);
        }
    }
}
