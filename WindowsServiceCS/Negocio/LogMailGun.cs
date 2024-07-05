using LogMailGunSvc.Services;
using LogMailGunSvc.Utils;
using System;
using System.Data;

namespace LogMailGunSvc.Negocio
{
    public class LogMailGun
    {
        public void Inicio()
        {
            LogUtil.Logger($"\nComienza Ejecucion!\t{DateTime.Now}");
            CatchEventsMailSended _eventsSended = new CatchEventsMailSended();
            DatabaseService _databaseService = new DatabaseService();
            string fechaInicio;
            string fechaFin;

            var registroMasActual = _databaseService.ObtenerRegistroMasActual();
            bool existeRegistroMasActual = (registroMasActual != null);

            if (existeRegistroMasActual)
            {
                //fechaInicio = FechaUtil.FormatearFecha(_eventsSended.ConvertirAFechaHora(Double.Parse(registroMasActual.Timestamp)), FechaUtil.formatoDDDDDMMYYYYHHMMSSUTC);
                fechaInicio = registroMasActual.Timestamp;
            }
            else
            {
                fechaInicio = FechaUtil.FormatearFecha(FechaUtil.SumarDiasFechas(DateTime.Today, -1), FechaUtil.formatoDDDDDMMYYYYHHMMSSUTC);
            }

            //fechaFin = FechaUtil.SumarDiasEnTimeStamp(fechaInicio, 1, FechaUtil.formatoDDDDDMMYYYYHHMMSSUTC);

            if (existeRegistroMasActual)
            {
                fechaFin = FechaUtil.SumarDiasEnTimeStamp(fechaInicio, 1, FechaUtil.formatoDDDDDMMYYYYHHMMSSUTC);
                var fechaInicioFormato = FechaUtil.FormatearFecha(FechaUtil.ConvertirAFechaHora(Double.Parse(fechaInicio)), FechaUtil.formatoDDDDDMMYYYYHHMMSSUTC);
                LogUtil.Logger($"FechaInicio = {fechaInicioFormato} \t FechaFin = {fechaFin}");
                LogUtil.Logger($"FechaInicio TimeStamp = {fechaInicio}");
            }
            else
            {
                fechaFin = FechaUtil.FormatearFecha(FechaUtil.SumarDiasFechas(FechaUtil.ConvertirStringADateTime(fechaInicio, FechaUtil.formatoDDDDDMMYYYYHHMMSSUTC), 1), FechaUtil.formatoDDDDDMMYYYYHHMMSSUTC);
                LogUtil.Logger($"FechaInicio = {fechaInicio} \t FechaFin = {fechaFin}");
            }
;

            try
            {
                var correos = _eventsSended.GetLogEntry(registroMasActual, fechaInicio, fechaFin);

                DatabaseService _dbService = new DatabaseService();
                DataTable tbl = _dbService.CargarData(correos);
                _dbService.InsertaData(tbl);

                int cont = 0;
                foreach (var correo in correos)
                {
                    cont++;
                }
                Console.WriteLine(cont);
            }
            catch (Exception ex)
            {
                LogUtil.Logger($"Error de ejecución {ex}");
            }
            LogUtil.Logger($"Fin Ejecucion!\t{DateTime.Now}\n");

        }

    }
}