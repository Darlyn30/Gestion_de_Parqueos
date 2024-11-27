using Servidor.Models;

namespace Servidor.Interfaces
{
    public interface Irepository<T>
    {
        List<T> GetAll();
        Task SP_register(int id);
        Task SP_eliminarRegister(string code, int id);

        string saveLogTxt();
        

    }
}
