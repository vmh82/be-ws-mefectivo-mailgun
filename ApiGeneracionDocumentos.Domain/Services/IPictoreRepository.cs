using ApiGeneracionDocumentos.Entity;
using ApiGeneracionDocumentos.Entity.Dto;
using System.Data;

namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface IPictoreRepository
    {
        List<DtoRutasPictore> GetRoutesPictorByIdClienteAndLotePictoreAndIdTramiteAsync(string identificacion, int? lotePictore, int idTramite);
    }
}