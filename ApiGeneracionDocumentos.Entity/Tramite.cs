namespace ApiGeneracionDocumentos.Entity
{
    public partial class Tramite
    {
        public int IdTramite { get; set; }
        public string NumeroTramite { get; set; } = "";
        public int IdCliente { get; set; }
        public decimal? MontoSeleccionado { get; set; }
        public decimal? MontoMaximo { get; set; }
        public decimal? MontoMinimo { get; set; }
        public decimal? CuotaSelecciona { get; set; }
        public decimal? CuotaMaxima { get; set; }
        public decimal? CuotaMinima { get; set; }
        public int? Plazo { get; set; }
        public string? NroSolicitud { get; set; }
        public decimal? MontoRecibir { get; set; }
        public decimal? Cuotamensual { get; set; }
        public int? PlazoFinal { get; set; }
        public DateTime? DiaPago { get; set; }
        public string Estado { get; set; } = "INA";
        public decimal? TasaNominal { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacionEstado { get; set; }
        public string? CodigoSeguro { get; set; }
        public decimal? ValorSeguro { get; set; }
        public int? IdCuentaCliente { get; set; }
        public string? NumeroCuenta { get; set; }
        public string? TipoSeguro { get; set; }
        public DateTime? FechaAceptaSeguro { get; set; }
        public string? NumeroCredito { get; set; }
        public DateTime? FechaAceptaCondicionCredito { get; set; }
        public string? XmlGeneracionTabla { get; set; }
        public string? XmlCuenta { get; set; }
        public DateTime? FechaAceptaDocumento { get; set; }
        public string? Comentario { get; set; }
        public string? TipoCuenta { get; set; }
        public DateTime FechaDesembolso { get; set; } = DateTime.Now;
        public bool? AceptaDesembolso { get; set; }
        public decimal? MontoFinanciado { get; set; }
        public decimal? MontoSeguroGarantia { get; set; }
        public bool? CrearCuenta { get; set; }
        public bool? EnvioAPictor { get; set; }
        public bool? EnvioClienteABankPlus { get; set; }
        public int? LotePictor { get; set; } = 0;
        public int IdFlujoWeb { get; set; } = 0;
        public bool? SeCreoCuenta { get; set; } = false;
        public int? IdOficina { get; set; }
        public int? IdAsesor { get; set; }
        public decimal? MontoBloqueoMicro { get; set; }
        public decimal? MontoBloqueoOro { get; set; }
        public int? IdInstitucion { get; set; }
        public bool? EsIndependiente { get; set; }
        public string? OfertaNoCliente { get; set; }
        public int? IdMotivo { get; set; }
        public bool? EsPorAgencia { get; set; }
        public int? IdAsesorFabrica { get; set; }
        public int? IdOficinaFabrica { get; set; }
        public bool? IngresoIdAsesor { get; set; }
        public decimal? MontoCompromisoMensual { get; set; }
        public short? PlazoCuenta { get; set; }
        public int? IdCuentaDebitar { get; set; }
        public short? DiaDebito { get; set; }
        public DateTime? FechaRenovacion { get; set; }
        public string? IPCliente { get; set; }
        public int? IdTipoTabla { get; set; }
    }
}
