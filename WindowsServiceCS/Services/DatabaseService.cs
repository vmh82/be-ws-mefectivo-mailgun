using LogMailGunSvc.Entities;
using LogMailGunSvc.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using LogMailGunSvc.Cifrado;
using SuiteGenerico.PlataformaDesarrollo.Utilitarios;
using System.Globalization;

namespace LogMailGunSvc.Services
{
    public class DatabaseService
    {

        private static readonly string csE = ConfigurationManager.AppSettings["CadenaConexionE"].ToString();
        private readonly string _symmetricKeyName = ConfigurationManager.AppSettings["SymmetricKeyName"].ToString();
        private readonly string _certificateName = ConfigurationManager.AppSettings["CertificateName"].ToString();

        private DataTable CreateDataTable()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add(new DataColumn("IdMensaje", typeof(string)));
            tbl.Columns.Add(new DataColumn("Evento", typeof(string)));
            tbl.Columns.Add(new DataColumn("FechaHoraText", typeof(string)));
            tbl.Columns.Add(new DataColumn("TimestampValue", typeof(string)));
            tbl.Columns.Add(new DataColumn("Remitente", typeof(string)));
            tbl.Columns.Add(new DataColumn("Receptor", typeof(Byte[])));
            tbl.Columns.Add(new DataColumn("Asunto", typeof(string)));
            tbl.Columns.Add(new DataColumn("JsonValue", typeof(Byte[])));

            return tbl;
        }


        public DataTable CargarData(List<MensajeLog> lst)
        {
            var tbl = CreateDataTable();
            foreach (var mensaje in lst)
            {
                var msj = "";
                if (!string.IsNullOrEmpty(mensaje.ToEmail))
                {
                    msj = mensaje.ToEmail;
                }

                DataRow dr = tbl.NewRow();
                dr["IdMensaje"] = mensaje.Id;
                dr["Evento"] = mensaje.Evento;
                dr["FechaHoraText"] = FechaUtil.FormatearFecha(mensaje.FechaHora, FechaUtil.formatoMDDYYYYHMMSSA);
                dr["TimestampValue"] = mensaje.Timestamp;
                dr["Remitente"] = mensaje.FromEmail;
                dr["Receptor"] = EncryptString(msj);
                dr["Asunto"] = mensaje.Subject;
                dr["JsonValue"] = EncryptString(mensaje.JsonBody.ToString().Replace("'", "`"));

                tbl.Rows.Add(dr);
            }
            return tbl;
        }

        private byte[] EncryptString(string input)
        {
            byte[] encryptedBytes;

            string _connectionString = Encriptador.Desencriptar(csE);
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand($"OPEN SYMMETRIC KEY {_symmetricKeyName} DECRYPTION BY CERTIFICATE {_certificateName};", connection))
                {
                    command.ExecuteNonQuery();
                }

                string cadena = string.Format("SELECT EncryptByKey(Key_GUID('{0}'), '{1}');", _symmetricKeyName, input);
                //using (var command = new SqlCommand($"SELECT EncryptByKey(Key_GUID('{_symmetricKeyName}'), '{input}');", connection))
                using (var command = new SqlCommand(cadena, connection))
                {
                    encryptedBytes = (byte[])command.ExecuteScalar();
                }

                using (var command = new SqlCommand($"CLOSE SYMMETRIC KEY {_symmetricKeyName};", connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            return encryptedBytes;
        }

        public void InsertaData(DataTable tbl)
        {
            RegisterData(tbl);
        }

        private static void RegisterData(DataTable tbl)
        {
            string cs = string.Empty;
            try
            {
                cs = Encriptador.Desencriptar(csE);
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    using (SqlBulkCopy objbulk = new SqlBulkCopy(con))
                    {
                        objbulk.DestinationTableName = ConfigurationManager.AppSettings["TablaDestino"].ToString();
                        objbulk.ColumnMappings.Add("IdMensaje", "IdMensaje");
                        objbulk.ColumnMappings.Add("Evento", "Evento");
                        objbulk.ColumnMappings.Add("FechaHoraText", "FechaHoraText");
                        objbulk.ColumnMappings.Add("TimestampValue", "TimestampValue");
                        objbulk.ColumnMappings.Add("Remitente", "Remitente");
                        objbulk.ColumnMappings.Add("Receptor", "Receptor");
                        objbulk.ColumnMappings.Add("Asunto", "Asunto");
                        objbulk.ColumnMappings.Add("JsonValue", "JsonValue");

                        objbulk.WriteToServer(tbl);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                LogUtil.Logger($"Error ingresando datos en InsertaData: {ex}");
            }
        }

        public MensajeLog ObtenerRegistroMasActual()
        {
            MensajeLog _msj = null;

            string cs = string.Empty;
            try
            {
                cs = Encriptador.Desencriptar(csE);
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string query = $"SELECT TOP 1 * FROM {ConfigurationManager.AppSettings["TablaDestino"]} Order by TimestampValue desc";
                    SqlCommand command = new SqlCommand(query, con);

                    try
                    {
                        con.Open();
                        var _timestamp = command.ExecuteScalar();

                        using (DbDataReader rdr = command.ExecuteReader(CommandBehavior.SequentialAccess))
                        {

                            // Iterate through the collection of Contact items.
                            while (rdr.Read())
                            {
                                _msj = new MensajeLog
                                {
                                    Id = rdr["IdMensaje"].ToString(),
                                    Evento = rdr["Evento"].ToString(),
                                    FechaHora = DateTime.Parse(rdr["FechaHoraText"].ToString(), new CultureInfo("en-US")),
                                    Timestamp = rdr["TimestampValue"].ToString(),
                                    FromEmail = rdr["Remitente"].ToString(),
                                    ToEmail = rdr["Receptor"].ToString(),
                                    Subject = rdr["Asunto"].ToString(),
                                    JsonBody = rdr["JsonValue"].ToString()
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogUtil.Logger($"Error de ejecucion en base: {ex}");
                    }
                    finally
                    {
                        con.Close();
                    }


                }
            }
            catch (Exception ex)
            {
                LogUtil.Logger($"Error de conexion de base: {ex}");
            }

            return _msj;
        }

    }
}
