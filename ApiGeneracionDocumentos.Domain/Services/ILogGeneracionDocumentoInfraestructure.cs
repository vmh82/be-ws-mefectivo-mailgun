using ApiGeneracionDocumentos.Entity;

namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface ILogGeneracionDocumentoInfraestructure
    {
        Task CreateLog(LogGeneracionDocumento logGeneracionDocumento);
        Task UpdateLog(LogGeneracionDocumento logGeneracionDocumento);
        Task<LogGeneracionDocumento> GetLogGeneracionByIdTramite(int idTramite);
        Task<LogGeneracionDocumento> GetLogGeneracionWithStatusZeroByIdTramite(int idTramite);
        Task<List<LogGeneracionDocumento>> GetLogsGeneracionByEstado(bool estado);
        Task ResetLogs();
        Task DeleteLogs();
    }
}
