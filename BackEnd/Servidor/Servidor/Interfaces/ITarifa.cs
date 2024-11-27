namespace Servidor.Interfaces
{
    public interface ITarifa
    {
        decimal calcTarifa(int vehiculoId, string formato, int cantidad);
    }
}
