using System.ComponentModel.DataAnnotations;

namespace ApiGeneracionDocumentos.Entity.Dto
{
    public class DtoErrorDB
    {
        [Key]
        public int IdTramite { get; set; }
        public List<DtoErrorGeneracionInterno> ErrorGeneracion { get; set; } = new List<DtoErrorGeneracionInterno>();
        public List<DtoErrorRutaCentralInterno> ErrorRutaCentral { get; set; } = new List<DtoErrorRutaCentralInterno>();
        public List<string> ErrorGeneracionFront { get; set; } = new List<string>();
        public List<string> ErrorRutaCentralFront { get; set; } = new List<string>();
    }

    public class DtoErrorGeneracionInterno
    {
        [Key]
        public string? Codigo { get; set; }
        public bool Estado { get; set; } = true;
        public string? Error { get; set; }
    }

    public class DtoErrorRutaCentralInterno
    {
        [Key]
        public string? Documento { get; set; }
        public bool Estado { get; set; } = true;
        public string? Error { get; set; }
    }
}
