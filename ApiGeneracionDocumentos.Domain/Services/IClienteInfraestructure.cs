using ApiGeneracionDocumentos.Entity;

namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface IClienteInfraestructure
    {
        string GetActividadEconomicaCliente(string identificacion);
        Task<Cliente> GetClienteByIdentificacion(string identificacion);
        Task<Cliente> GetClienteByIdCliente(int idCliente);
    }
}