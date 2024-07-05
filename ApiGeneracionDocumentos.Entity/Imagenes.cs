namespace ApiGeneracionDocumentos.Entity
{
    public partial class Imagenes
    {
        public Imagenes()
        {
            this.IMAGENES_CARPETA = new HashSet<ImagenesCarpeta>();
        }

        public int IMAG_CODIGO { get; set; }
        public int? RUDI_CODIGO { get; set; }
        public int? EMPR_CODIGO { get; set; }
        public int? TDOC_CODIGO { get; set; }
        public int? USER_CODIGO { get; set; }
        public DateTime? IMAG_FECHA { get; set; }
        public string IMAG_DESCRIPCION { get; set; }
        public string IMAG_RUTA { get; set; }
        public int? IMAG_BASE { get; set; }
        public int? IMAG_ESTADO { get; set; }
        public string IMAG_NOMBRE { get; set; }
        public string IMAG_BRANCH { get; set; }
        public int? IMAG_DELO_CODIGO { get; set; }
        public DateTime? IMAG_FECHA_CENTRALIZACION { get; set; }
        public int? CANA_CODIGO { get; set; }
        public virtual ICollection<ImagenesCarpeta> IMAGENES_CARPETA { get; set; }
        public virtual RutaDisco RUTA_DISCO { get; set; }
    }
}
