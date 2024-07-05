namespace ApiGeneracionDocumentos.Entity
{
    public partial class RutaDisco
    {
        public RutaDisco()
        {
            IMAGENES = new HashSet<Imagenes>();
        }

        public int RUDI_CODIGO { get; set; }
        public int? EMPR_CODIGO { get; set; }
        public int? RUTA_CODIGO { get; set; }
        public DateTime? UBIA_FECHA { get; set; }
        public int? UBIA_MAX_NRO { get; set; }
        public virtual ICollection<Imagenes> IMAGENES { get; set; }
        public virtual Ruta RUTA { get; set; }
    }
}
