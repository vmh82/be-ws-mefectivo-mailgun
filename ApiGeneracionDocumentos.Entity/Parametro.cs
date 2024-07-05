namespace ApiGeneracionDocumentos.Entity
{
    public partial class Parametro
    {
        public int IdParametro { get; set; }
        public string Modulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public int? ValorEntero { get; set; }
        public decimal? ValorDecimal { get; set; }
        public string? ValorCadena { get; set; }
        public DateTime? ValorFecha { get; set; }
        public string? Estado { get; set; } = "INA";
        public string? Observaciones { get; set; } = string.Empty;
        public bool? EsPorInstitucion { get; set; }
        public bool? EsEncriptado { get; set; }
        public bool? EsCentralizado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public int? IdOficinaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public int? IdUsuarioActualizacion { get; set; }
        public int? IdOficinaActualizacion { get; set; }
    }
}
