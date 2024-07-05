using ApiGeneracionDocumentos.Entity;

namespace ApiGeneracionDocumentos.Test.FakeData
{
    public class ConfiguracionFakeData
    {
        public static List<Parametro> GetFakeParametrosList()
        {
            return new List<Parametro>()
            {
                new() {
                    IdParametro = 1,
                    Modulo = "BNK",
                    Nombre = "FECHASISTEMA",
                    Descripcion = "Fecha de transacción del sistema",
                    Tipo = "FECH",
                    ValorEntero = null,
                    ValorDecimal = null,
                    ValorCadena = null,
                    ValorFecha = DateTime.Now,
                    Estado = "ACT",
                    Observaciones = "",
                    EsPorInstitucion = false,
                    EsEncriptado = false,
                    EsCentralizado = false,
                    FechaCreacion = new DateTime(2009,04,08),
                    IdUsuarioCreacion = null,
                    IdOficinaCreacion = null,
                    FechaActualizacion = DateTime.Now,
                    IdUsuarioActualizacion = 1828,
                    IdOficinaActualizacion = 227

                },

                new() {
                    IdParametro = 1829,
                    Modulo = "BNK",
                    Nombre = "RUTADOCDIGITAL",
                    Descripcion = "Ruta para la ubicacion de los documentos digitales",
                    Tipo = "CADE",
                    ValorEntero = null,
                    ValorDecimal = null,
                    ValorCadena = "\\\\10.10.0.56\\D$\\SvcRapidito\\ImagenesCarpeta\\MP",
                    ValorFecha = null,
                    Estado = "ACT",
                    Observaciones = "RUTA PARA DOCUMENTOS DIGITALES",
                    EsPorInstitucion = false,
                    EsEncriptado = false,
                    EsCentralizado = false,
                    FechaCreacion = new DateTime(2018,05,16),
                    IdUsuarioCreacion = null,
                    IdOficinaCreacion = null,
                    FechaActualizacion = new DateTime(2018,05,16),
                    IdUsuarioActualizacion = null,
                    IdOficinaActualizacion = null

                }
            };
        }
    }
}
