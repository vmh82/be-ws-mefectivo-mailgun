namespace ApiGeneracionDocumentos.Entity
{
    public partial class Ruta
    {
        public Ruta()
        {
            RUTA_DISCO = new HashSet<RutaDisco>();
        }

        public int RUTA_CODIGO { get; set; }
        public string RUTA_PATH { get; set; }
        public int? RUTA_CONTADOR { get; set; }
        public virtual ICollection<RutaDisco> RUTA_DISCO { get; set; }
    }
}
