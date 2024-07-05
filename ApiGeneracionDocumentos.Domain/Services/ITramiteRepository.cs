using ApiGeneracionDocumentos.Entity;

namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface ITramiteRepository
    {
        Task<Tramite> GetTramiteByIdTramite(int idTramite);
        Task<List<Tramite>> GetTramitesByIdentificacionAndFlujoWeb(int idCliente, string codigoFlujoWeb);
        Task<List<Tramite>> GetTramitesByDateRange(DateTime fechaInicio, DateTime fechaFin);
        Task<Tramite> GerTramiteByNroSolicitud(string nroSolicitud);
    }
}
