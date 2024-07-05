using ApiGeneracionDocumentos.Entity.Dto;

namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface IPictoreInfraestructure
    {
        List<DtoRutasPictore> GetRutasPictore(string identificacion, int? lotePictore, int idTramite);
    }
}