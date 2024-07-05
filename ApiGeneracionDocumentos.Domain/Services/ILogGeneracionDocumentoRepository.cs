using ApiGeneracionDocumentos.Entity;

namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface ILogGeneracionDocumentoRepository
    {
        Task<int> AddLog(LogGeneracionDocumento logGeneracionDocumento);
        Task<int> UpdateLog(LogGeneracionDocumento logGeneracionDocumento);
        Task<LogGeneracionDocumento> GetLogGeneracionByIdTramite(int idTramite);
        Task<LogGeneracionDocumento> GetLogGeneracionWithStatusZeroByIdTramite(int idTramite);
        Task<List<LogGeneracionDocumento>> GetLogsGeneracionByEstado(bool estado);
        Task ResetLogs();
        Task DeleteLogs();
    }
}