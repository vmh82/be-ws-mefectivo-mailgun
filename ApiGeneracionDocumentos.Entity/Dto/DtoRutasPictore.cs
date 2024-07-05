using System.ComponentModel.DataAnnotations;

namespace ApiGeneracionDocumentos.Entity.Dto
{
    public class DtoRutasPictore
    {
        [Key]
        public int IdTramite { get; set; }
        public string? RutaLocal { get; set; }
        public string? RutaCentral { get; set; }
        public string? NombreDocumento { get; set; }
    }
}
