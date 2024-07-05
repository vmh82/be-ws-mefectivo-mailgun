using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Entity;
using ApiGeneracionDocumentos.Repository.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiGeneracionDocumentos.Repository.Services
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDbContextFactory<CreditoWebContext> _context;
        private readonly IDbContextFactory<ClienteContext> _clienteContext;
        public ClienteRepository(IDbContextFactory<CreditoWebContext> context, IDbContextFactory<ClienteContext> clienteContext) 
        {
            _context = context;
            _clienteContext = clienteContext;
        }

        public DataSet GetActividadEconomicaCliente(string identificacion)
        {
            try
            {
                return ExecuteProcedureWithParameter(_clienteContext.CreateDbContext(), "pObtieneActividadEconomicaCliente", identificacion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cliente> GetClienteByIdentificacion(string identificacion)
        {
            return await _context.CreateDbContext().Cliente.Where(cliente => cliente.Identificacion == identificacion).SingleOrDefaultAsync() ?? new Cliente();
        }

        public async Task<Cliente> GetClienteByIdCliente(int idCliente)
        {
            return await _context.CreateDbContext().Cliente.Where(cliente => cliente.IdCliente == idCliente).SingleOrDefaultAsync() ?? new Cliente();
        }

        private static DataSet ExecuteProcedureWithParameter(DbContext context, string procedimiento, string parameterValue)
        {
            try
            {
                DataSet dataSet = new();
                context.Database.OpenConnection();
                context.Database.SetCommandTimeout(TimeSpan.FromMinutes(2));
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = string.Format(
                    "exec " + procedimiento + " '{0}'", parameterValue
                );
                command.ExecuteNonQuery();

                using SqlDataAdapter adapter = new();
                adapter.SelectCommand = (SqlCommand)command;

                adapter.Fill(dataSet);
                context.Database.CloseConnection();
                return dataSet;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
