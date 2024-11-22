namespace Servidor.Interfaces
{
    public interface Irepository<T>
    {
        List<T> GetAll();
        void Add(T item);
    }
}
