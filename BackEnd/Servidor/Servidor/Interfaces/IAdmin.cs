using Servidor.Models;

namespace Servidor.Interfaces
{
    public interface IAdmin
    {
        List<Cuenta> GetCuentas();
        void Set(Cuenta model);
        
    }
}
