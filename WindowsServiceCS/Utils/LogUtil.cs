using System;
using System.IO;

namespace LogMailGunSvc.Utils
{
    public static class LogUtil
    {

        public static void Logger(string lines)
        {
            string path = $"{UtilPath.GetRutaLog()}";

            string fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_Logs.txt";
            try
            {
                StreamWriter file = new StreamWriter($"{path}/{fileName}", true);
                file.WriteLine(DateTime.Now.ToString() + ": " + lines);
                file.Close();
            }
            catch (Exception) { }
        }
        public static void LoggerService(string lines)
        {
            string path = $"{UtilPath.GetRutaService()}";

            string fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_ServiceLogs.txt";
            try
            {
                StreamWriter file = new StreamWriter($"{path}/{fileName}", true);
                file.WriteLine(DateTime.Now.ToString() + ": " + lines);
                file.Close();
            }
            catch (Exception) { }
        }
    }
}
