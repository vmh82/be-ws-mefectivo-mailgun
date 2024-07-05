using ApiGeneracionDocumentos.Entity;

namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface IVersionDocumentoFlujoWebRepository
    {
        Task<List<VersionDocumentoFlujoWeb>> GetVersionesByIdFlujoWebOrderByFechaVigencia(int idFlujoWeb);
    }
}