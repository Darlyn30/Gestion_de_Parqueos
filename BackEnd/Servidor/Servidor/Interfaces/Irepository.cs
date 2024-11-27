using Servidor.Models;

namespace Servidor.Interfaces
{
    public interface Irepository<T>
    {
        List<T> GetAll();
        Task<List<IngresoAuto>> SP_register(int id);
        Task<List<IngresoAuto>> SP_eliminarRegister(string code, int id);

        string saveLogTxt();

        

    }
}
