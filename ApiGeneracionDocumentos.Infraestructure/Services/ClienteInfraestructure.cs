using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Entity;
using System.Data;

namespace ApiGeneracionDocumentos.Infraestructure.Services
{
    public class ClienteInfraestructure : IClienteInfraestructure
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteInfraestructure(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public string GetActividadEconomicaCliente(string identificacion)
        {
            DataSet ds = _clienteRepository.GetActividadEconomicaCliente(identificacion);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["ActividadEconomica"].ToString()!;
            }
            else
            {
                return string.Empty;
            }                
        }

        public async Task<Cliente> GetClienteByIdCliente(int idCliente)
        {
            return await _clienteRepository.GetClienteByIdCliente(idCliente);
        }

        public async Task<Cliente> GetClienteByIdentificacion(string identificacion)
        {
            return await _clienteRepository.GetClienteByIdentificacion(identificacion);
        }
    }
}
