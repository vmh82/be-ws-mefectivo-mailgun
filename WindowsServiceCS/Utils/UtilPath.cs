using System.IO;


namespace LogMailGunSvc.Utils
{
    public static class UtilPath
    {
        public static void VerifyDir(string path)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists)
                {
                    dir.Create();
                }
            }
            catch { }
        }

        private static string GetRutaBase()
        {
            return "C:/LogMailGun";
        }

        public static string GetRutaLog()
        {
            string path = $"{GetRutaBase()}/Log";
            VerifyDir(path);
            return path;
        }

        public static string GetRutaService()
        {
            string path = $"{GetRutaBase()}/Service";
            VerifyDir(path);
            return path;
        }

        public static string GetRutaJson()
        {
            string path = $"{GetRutaBase()}/Json";
            VerifyDir(path);
            return path;
        }
    }
}
