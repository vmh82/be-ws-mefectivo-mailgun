using ApiGeneracionDocumentos.Entity;

namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface IReconocimientoFacialRepository
    {
        Task<ReconocimientoFacial> GetReconocimientoFacialByNumeroTramite(string numeroTramite);
    }
}