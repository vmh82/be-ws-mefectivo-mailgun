using ApiGeneracionDocumentos.Entity.Dto;

namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface IVersionDetalleDocumentoFlujoWebRepository
    {
        Task<List<DtoDocumentoFlujoWeb>> GetDetalleVersionesByIdVersionDocumento(int idVersionDocumentoFlujoWeb, DateTime fechaDesembolso);
    }
}