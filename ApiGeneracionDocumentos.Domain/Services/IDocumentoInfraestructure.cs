using ApiGeneracionDocumentos.Entity.Dto;

namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface IDocumentoInfraestructure
    {
        Task<List<DtoDocumentoFlujoWeb>> GetDocumentosToGenerateByFlujoWeb(int idFlujoWeb, DateTime fechaDesembolso);
        Task<IEnumerable<DtoResponse>> DocumentGenerationByTramites(List<DtoTramite> tramites, bool generarDocumentosFaltantes);
        Task<IEnumerable<DtoResponse>> CopyTotalDocumentInIndividualDocumentsByIdTramite(DtoRequest dtoRequest);
        Task<IEnumerable<DtoResponse>> CopyTotalDocumentInIndividualDocumentsByDates(DtoRequest dtoRequest);
    }
}