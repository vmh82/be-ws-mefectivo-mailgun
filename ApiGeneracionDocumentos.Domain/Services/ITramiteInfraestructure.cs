using ApiGeneracionDocumentos.Entity;
using ApiGeneracionDocumentos.Entity.Dto;

namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface ITramiteInfraestructure
    {
        Task<Tramite> GetTramiteByIdTramite(int idTramite);
        Task<DtoTramite> GetDtoTramiteByIdTramite(int idTramite);
        Task<DtoTramite> GetTramiteByIdentificacionAndFlujoWebAndFechaDesem(string identificacion, string codigoFlujoWeb, DateTime fechaDesembolso);
        Task<List<DtoTramite>> GetTramitesByDateRange(DateTime fechaInicio, DateTime fechaFin);
        Task<Tramite> GerTramiteByNroSolicitud(string nroSolicitud);
    }
}
