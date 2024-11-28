namespace Servidor.Interfaces
{
    public interface ITarifa
    {
        decimal calcTarifa(int vehiculoId, string formato, int cantidad);
        decimal GetMonto(DateTime horaEntrada, int vehiculoId);
    }
}
