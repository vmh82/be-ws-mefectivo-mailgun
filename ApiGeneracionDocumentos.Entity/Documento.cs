namespace ApiGeneracionDocumentos.Entity
{
    public partial class Documento
    {
        public int IdDocumento { get; set; }
        public string Modulo { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string NombreDocumento { get; set; } = string.Empty;
        public string Procedimiento { get; set; } = string.Empty;
        public bool EsWord { get; set; } = false;
        public bool AgrupaDocumentos { get; set; } = false;

    }
}
