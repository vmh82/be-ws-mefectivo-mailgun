namespace ApiGeneracionDocumentos.Entity
{
    public class LogGeneracionDocumento
    {
        public int IdLogGeneracionDocumento { get; set; }
        public int IdTramite { get; set; }
        public int IdCliente { get; set; }
        public int IdFlujoWeb { get; set; }
        public string? NroSolicitud { get; set; }
        public string? Identificacion { get; set; }
        public int? LotePictor { get; set; }
        public DateTime? FechaDesembolso { get; set; }
        public DateTime? FechaProcesoInicio { get; set; } = DateTime.Now;
        public DateTime? FechaProcesoFin { get; set; }
        public bool Estado { get; set; } = false;
        public string? Error { get; set; }
        public string? ArchivosCopiados { get; set; }

    }
}
