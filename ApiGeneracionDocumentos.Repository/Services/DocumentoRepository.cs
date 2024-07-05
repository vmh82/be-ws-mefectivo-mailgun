using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Entity;
using ApiGeneracionDocumentos.Repository.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SuiteGenerico.PlataformaDesarrollo.Entidades;
using System.Data;

namespace ApiGeneracionDocumentos.Repository.Services
{
    public class DocumentoRepository : IDocumentoRepository
    {
        private readonly IDbContextFactory<CreditoWebContext> _contextCreditoWeb;
        private readonly IDbContextFactory<CreditoContext> _contextCredito;
        private readonly IDbContextFactory<SolicitudContext> _contextSolicitud;
        public DocumentoRepository(IDbContextFactory<CreditoWebContext> creditoWebContext, IDbContextFactory<CreditoContext> creditoContext, IDbContextFactory<SolicitudContext> solicitudContext)
        {
            _contextCreditoWeb = creditoWebContext;
            _contextCredito = creditoContext;
            _contextSolicitud = solicitudContext;
        }

        public async Task<Cliente> GetClienteByIdentificacion(string identificacion)
        {
            return await _contextCreditoWeb.CreateDbContext().Cliente.Where(cliente => cliente.Identificacion == identificacion).SingleOrDefaultAsync() ?? new Cliente();
        }

        public DataSet GetInformationByProcedure(string codigoModulo, string procedimiento, string numeroSolicitud)
        {
            try
            {
                DataSet dataSet = new();
                switch (Enumerados.ObtenerEnumModulo(codigoModulo))
                {
                    case Modulo.Credito:
                        dataSet = ExecuteProcedureWithParameter(_contextCredito.CreateDbContext(), procedimiento, numeroSolicitud);
                        break;
                    case Modulo.Solicitudes:
                        dataSet = ExecuteProcedureWithParameter(_contextSolicitud.CreateDbContext(), procedimiento, numeroSolicitud);
                        break;
                    case Modulo.CreditoWeb:
                        dataSet = ExecuteProcedureWithParameter(_contextCreditoWeb.CreateDbContext(), procedimiento, numeroSolicitud);
                        break;
                }

                return dataSet;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        private static DataSet ExecuteProcedureWithoutParameters(DbContext context, string procedimiento)
        {
            try
            {
                DataSet dataSet = new();
                context.Database.OpenConnection();
                context.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = string.Format(
                    "exec " + procedimiento
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

        public DataSet CentralDocumentGenerator()
        {
            return ExecuteProcedureWithoutParameters(_contextCreditoWeb.CreateDbContext(), "pGeneraDocumentoPictor");
        }

        public async Task<Documento> GetDocumentoByCodigo(string codigo)
        {
            return await _contextCreditoWeb.CreateDbContext().Documento.Where(documento => documento.Codigo == codigo).FirstOrDefaultAsync() ?? new Documento();
        }
    }
}
