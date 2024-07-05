namespace ApiGeneracionDocumentos.Entity
{
    public partial class ImagenesCarpeta
    {
        public int IMCA_CODIGO { get; set; }
        public int IMAG_CODIGO { get; set; }
        public int CARP_CODIGO { get; set; }
        public virtual Imagenes IMAGENE { get; set; }
    }
}
