using ApiGeneracionDocumentos.Entity;
using System.Data;

namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente> GetClienteByIdentificacion(string identificacion);
        Task<Cliente> GetClienteByIdCliente(int idCliente);
        DataSet GetActividadEconomicaCliente(string identificacion);
    }
}