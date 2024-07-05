namespace ApiGeneracionDocumentos.Entity
{
    public partial class ReconocimientoFacial
    {
        public int IdReconocimientoFacial { get; set; }
        public string Guid { get; set; }
        public string Clave { get; set; }
        public string NumeroTramite { get; set; }
        public string NroSolicitud { get; set; }
        public string NumeroCredito { get; set; }
        public string Identificacion { get; set; }
        public DateTime FechaEnvioToken { get; set; }
        public string TokenEncriptado { get; set; }
        public DateTime? FechaAprobacionToken { get; set; }
        public DateTime? FechaExpiracionToken { get; set; }
        public string NombreCliente { get; set; }
        public long? IdOperation { get; set; }
        public string? UserName { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string Mensaje { get; set; }
    }
}
