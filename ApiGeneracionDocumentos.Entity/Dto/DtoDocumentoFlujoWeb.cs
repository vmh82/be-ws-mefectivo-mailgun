namespace ApiGeneracionDocumentos.Entity.Dto
{
    public class DtoDocumentoFlujoWeb
    {
        public int IdDocumento { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Modulo { get; set; } = string.Empty;
        public string NombreDocumento { get; set; } = string.Empty;
        public string Procedimiento { get; set; } = string.Empty;
        public byte[] ObjetoFormato { get; set; } = Array.Empty<byte>();
        public string NroSolicitud { get; set; } = string.Empty;
        public DateTime FechaDesembolso { get; set; }
    }
}
