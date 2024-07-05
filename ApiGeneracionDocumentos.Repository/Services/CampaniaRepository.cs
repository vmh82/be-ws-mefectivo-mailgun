using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Repository.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiGeneracionDocumentos.Repository.Services
{
    public class CampaniaRepository : ICampaniaRepository
    {
        private readonly IDbContextFactory<GestionContext> _gestionContext;
        public CampaniaRepository(IDbContextFactory<GestionContext> gestionContext)
        {
            _gestionContext = gestionContext;
        }

        public DataSet GetNombreCampaniaByCodigoCampania(string codigoCampania)
        {
            try
            {
                return ExecuteProcedureWithParameter(_gestionContext.CreateDbContext(), "pConsultaNombreCampania", codigoCampania);
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
    }
}
