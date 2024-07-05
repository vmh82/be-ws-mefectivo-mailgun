using System;
using System.Globalization;

namespace LogMailGunSvc.Utils
{
    public static class FechaUtil
    {

        public static string formatoDDDDDMMYYYYHHMMSSUTC = "ddd, dd MMM yyyy HH:mm:ss UTC";
        public static string formatoMDDYYYYHMMSSA= "M/dd/yyyy H:mm:ss";

        public static DateTime SumarDiasFechas(DateTime fecha, int dias)
        {
            return fecha.AddDays(dias);
        }

        public static string FormatearFecha(DateTime fecha, string formato)
        {
            return fecha.ToString(formato, new CultureInfo("en-US"));

        }

        public static DateTime ConvertirStringADateTime(string fecha, string formato)
        {

            return DateTime.ParseExact(fecha, formato, new CultureInfo("en-US"));
        }

        public static DateTime ConvertirAFechaHora(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static string SumarDiasEnTimeStamp(string timestamp, int dias, string formatoARetornar)
        {
            var fechaString = FormatearFecha(ConvertirAFechaHora(Double.Parse(timestamp)), formatoARetornar);
            return FormatearFecha(SumarDiasFechas(ConvertirStringADateTime(fechaString, formatoARetornar), dias), formatoARetornar);
        }
    }
}
