namespace ApiGeneracionDocumentos.Entity
{
    public partial class VersionDetalleDocumentoFlujoWeb
    {
        public int IdVersionDetalleDocumentoFlujoWeb { get; set; }
        public int IdVersionDocumentoFlujoWeb { get; set; }
        public int IdDocumento { get; set; }
        public int Orden { get; set; }
        public int OrdenInicial { get; set; }
        public int NroCopias { get; set; }
        public bool EnvioPictor { get; set; } = false;
    }
}
