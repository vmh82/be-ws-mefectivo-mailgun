using ApiGeneracionDocumentos.Entity;
using System.Data;

namespace ApiGeneracionDocumentos.Test.FakeData
{
    public class ClienteFakeData
    {

        readonly static List<Cliente> clients = new()
            {
                new () {
                    IdCliente = 1,
                    TipoIdentificacion = "CED",
                    Identificacion = "1804915617",
                    Naturaleza = "NAT",
                    Estado = "CACT",
                    Actualizado = true,
                    EsExcento = false,
                    EstadoRiesgo = "COPE",
                    FechaUltimaTransaccion = new DateTime(2024,05,13),
                    IdSectorOrganismoControl = 1,
                    FechaCreacion = new DateTime(2023,05,27),
                    IdUsuarioCreacion = 1,
                    IdOficinaCreacion = 1,
                    FechaActualizacion = new DateTime(2024,01,01),
                    IdUsuarioActualizacion = 1,
                    IdOficinaActualizacion = 1,
                    IdClienteBP = 1
                },
                new () {
                    IdCliente = 2,
                    TipoIdentificacion = "CED",
                    Identificacion = "1234567890",
                    Naturaleza = "NAT",
                    Estado = "CACT",
                    Actualizado = true,
                    EsExcento = false,
                    EstadoRiesgo = "COPE",
                    FechaUltimaTransaccion = new DateTime(2024,05,12),
                    IdSectorOrganismoControl = 1,
                    FechaCreacion = new DateTime(2023,05,20),
                    IdUsuarioCreacion = 2,
                    IdOficinaCreacion = 2,
                    FechaActualizacion = new DateTime(2023,01,01),
                    IdUsuarioActualizacion = 2,
                    IdOficinaActualizacion = 2,
                    IdClienteBP = 2
                }
            };
        public static DataSet StoreProcedurepObtieneActividadEconomicaCliente(string identificacion)
        {
            DataSet ds = new();
            DataTable dt = new();
            DataColumn dataColumn = new("ActividadEconomica");
            dt.Columns.Add(dataColumn);

            switch (identificacion)
            {
                case IDENTIFICATION_SAL:
                    dt.Rows.Add(SAL_ECONOMIC_ACTIVITY);
                    break;
                case IDENTIFICATION_VEN:
                    dt.Rows.Add(VEN_ECONOMIC_ACTIVITY);
                    break;
                default:
                    dt.Rows.Add(DBNull.Value);
                    break;
            }

            ds.Tables.Add(dt);
            return ds;
        }

        public static async Task<Cliente> GetClientByIdClient(int IdClient)
        {
            return await Task.Run(() => clients.Find(client => client.IdCliente == IdClient) ?? new Cliente()); 
        }

        public static async Task<Cliente> GetClientByIdentification(string identification)
        {
            return await Task.Run(() => clients.Find(client => client.Identificacion == identification) ?? new Cliente());
        }

        public const string IDENTIFICATION_SAL = "1804915617";
        public const string IDENTIFICATION_VEN = "1234567890";
        public const string IDENTIFICATION_EMPTY = "9876543210";

        public const string SAL_ECONOMIC_ACTIVITY = "Salario";
        public const string VEN_ECONOMIC_ACTIVITY = "Ventas";

        public const string EXCEPTION_MESSAGE = "Exception message client";
    }
}
