using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Entity;

namespace ApiGeneracionDocumentos.Infraestructure.Services
{
    public class LogGeneracionDocumentoInfraestructure : ILogGeneracionDocumentoInfraestructure
    {
        private readonly ILogGeneracionDocumentoRepository _logGeneracionDocumentoRepository;

        public LogGeneracionDocumentoInfraestructure(ILogGeneracionDocumentoRepository logGeneracionDocumentoRepository)
        {
            _logGeneracionDocumentoRepository = logGeneracionDocumentoRepository;
        }

        public async Task UpdateLog(LogGeneracionDocumento logGeneracionDocumento)
        {
            try
            {
                await _logGeneracionDocumentoRepository.UpdateLog(logGeneracionDocumento);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task CreateLog(LogGeneracionDocumento logGeneracionDocumento)
        {
            try
            {
                LogGeneracionDocumento logGeneracion = await GetLogGeneracionByIdTramite(logGeneracionDocumento.IdTramite);
                if (logGeneracion.IdLogGeneracionDocumento == 0)
                {
                    await _logGeneracionDocumentoRepository.AddLog(logGeneracionDocumento);
                }
                else
                {
                    logGeneracion.FechaProcesoInicio = logGeneracionDocumento.FechaProcesoInicio;
                    await _logGeneracionDocumentoRepository.UpdateLog(logGeneracion);
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<LogGeneracionDocumento> GetLogGeneracionByIdTramite(int idTramite)
        {
            return await _logGeneracionDocumentoRepository.GetLogGeneracionByIdTramite(idTramite);
        }

        public async Task<List<LogGeneracionDocumento>> GetLogsGeneracionByEstado(bool estado)
        {
            return await _logGeneracionDocumentoRepository.GetLogsGeneracionByEstado(estado);
        }

        public async Task ResetLogs()
        {
            await _logGeneracionDocumentoRepository.ResetLogs();
        }
        public async Task DeleteLogs()
        {
            await _logGeneracionDocumentoRepository.DeleteLogs();
        }

        public async Task<LogGeneracionDocumento> GetLogGeneracionWithStatusZeroByIdTramite(int idTramite)
        {
            return await _logGeneracionDocumentoRepository.GetLogGeneracionWithStatusZeroByIdTramite(idTramite);
        }
    }
}
