using System.Data;

namespace ApiGeneracionDocumentos.Entity.Dto
{
    public class DtoTramite
    {
        public int IdTramite { get; set; }
        public int IdCliente { get; set; }
        public int? LotePictor { get; set; } = 0;
        public int IdFlujoWeb { get; set; } = 0;
        public string? NroSolicitud { get; set; }
        public string? NumeroTramite { get; set; }
        public DateTime FechaDesembolso { get; set; } = DateTime.Now;
        public decimal? ValorSeguro { get; set; }
        public bool? SeCreoCuenta { get; set; } = false;
        public string? Identificacion { get; set; }
        public string? CodigoCampania { get; set; }
    }
}
