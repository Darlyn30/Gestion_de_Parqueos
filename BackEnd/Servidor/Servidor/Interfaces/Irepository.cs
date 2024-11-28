using Servidor.Models;

namespace Servidor.Interfaces
{
    public interface Irepository<T>
    {
        List<T> GetAll();
        void SP_register(int id);
        void SP_eliminarRegister(string code, int id);

        string saveLogTxt();
        

    }
}
