namespace ApiGeneracionDocumentos.Entity.Dto
{
    public class DtoGeneration
    {
        public int IdTramite { get; set; } = 0;
        public bool Estado { get; set; } = true;
        public DtoErrorDB Error { get; set; } = new();
    }
}
