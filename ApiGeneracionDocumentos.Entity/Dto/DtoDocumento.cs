using System.Data;

namespace ApiGeneracionDocumentos.Entity.Dto
{
    public class DtoDocumento
    {
        public string Codigo { get; set; } = string.Empty;
        public string NroSolicitud { get; set; } = string.Empty;
        public byte[] ObjetoFormato { get; set; } = Array.Empty<byte>();
        public string PathDoc { get; set; } = string.Empty;
        public string PathRtf { get; set; } = string.Empty;
        public string CodigoCampania { get; set; } = string.Empty;
        public string PathPdf { get; set; } = string.Empty;
        public DateTime? FechaDesembolso { get; set; }
        public DataSet InformacionSP { get; set; } = new DataSet();
        public decimal? ValorSeguro { get; set; }
        public bool? SeCreoCuenta { get; set; }
        public string? Identificacion { get; set; }
    }
}
