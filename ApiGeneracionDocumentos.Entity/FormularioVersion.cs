namespace ApiGeneracionDocumentos.Entity
{
    public partial class FormularioVersion
    {
        public int IdFormularioVersion { get; set; }
        public int IdFormulario { get; set; }
        public int IdtipoDocumento { get; set; }
        public bool EsReimprimible { get; set; }
        public bool? MarcaDeAgua { get; set; }
        public int? IdMarcaAgua { get; set; }
        public DateTime FechaVigencia { get; set; }
        public byte[]? ObjetoFormato { get; set; }
        public bool EsActivo { get; set; }
        public string Modulo { get; set; } = string.Empty;
        public string? NombreSP { get; set; } = string.Empty;

    }
}
