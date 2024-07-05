namespace ApiGeneracionDocumentos.Entity
{
    public partial class VersionDocumentoFlujoWeb
    {
        public int IdVersionDocumentoFlujoWeb { get; set; }
        public int IdFlujoWeb { get; set; }
        public DateTime FechaVigencia { get; set; }
        public bool Estado { get; set; } = false;
        public DateTime FechaCreacion { get; set; }

    }
}
