namespace ApiGeneracionDocumentos.Entity.Dto
{
    public class DtoRequest
    {
        public string Identificacion { get; set; } = string.Empty;
        public string CodigoFlujoWeb { get; set; } = string.Empty;
        public DateTime FechaDesembolso { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdTramite { get; set; } = 0;
        public bool GenerarDocumentosFaltantes { get; set; } = true;
        public string? IdFirma { get; set; } 
    }
}
