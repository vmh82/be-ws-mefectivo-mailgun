namespace ApiGeneracionDocumentos.Entity.Dto
{
    public class DtoResponse
    {
        public int IdTramite { get; set; }        
        public List<string> ErrorGeneracion { get; set; } = new List<string>();
        public List<string> ErrorRutaCentral { get; set; } = new List<string>();
    }
}
