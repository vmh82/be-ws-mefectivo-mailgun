namespace ApiGeneracionDocumentos.Entity.Dto
{
    public class DtoInformacionDocumentos
    {
        public int IdFlujoWeb { get; set; }
        public string? Identificacion { get; set; }
        public string? NroSolicitud { get; set; }
        public string? CodigoCampania { get; set; }
        public int IdTramite { get; set; }
        public int? LotePictor { get; set; }
        public DateTime FechaDesembolso { get; set; }
        public decimal? ValorSeguro { get; set; }
        public bool? SeCreoCuenta { get; set; }
        public List<DtoRutasPictore> RutasPictore { get; set; } = new List<DtoRutasPictore>();
        public string RutaDocDigital { get; set; } = string.Empty;
    }
}
