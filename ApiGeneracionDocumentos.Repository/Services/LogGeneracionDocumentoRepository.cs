using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Entity;
using ApiGeneracionDocumentos.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiGeneracionDocumentos.Repository.Services
{
    public class LogGeneracionDocumentoRepository : ILogGeneracionDocumentoRepository
    {
        private readonly IDbContextFactory<CreditoWebContext> _contextFactory;
        public LogGeneracionDocumentoRepository(IDbContextFactory<CreditoWebContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<int> AddLog(LogGeneracionDocumento logGeneracionDocumento)
        {
            try
            {
                CreditoWebContext context = _contextFactory.CreateDbContext();
                context.LogGeneracionDocumento.Add(logGeneracionDocumento);
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<int> UpdateLog(LogGeneracionDocumento logGeneracionDocumento)
        {
            try
            {
                CreditoWebContext context = _contextFactory.CreateDbContext();
                context.LogGeneracionDocumento.Update(logGeneracionDocumento);
                return await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LogGeneracionDocumento> GetLogGeneracionByIdTramite(int idTramite)
        {
            return await _contextFactory.CreateDbContext().LogGeneracionDocumento.Where(log => log.IdTramite == idTramite).FirstOrDefaultAsync() ?? new LogGeneracionDocumento();

        }

        public async Task<List<LogGeneracionDocumento>> GetLogsGeneracionByEstado(bool estado)
        {
            return await _contextFactory.CreateDbContext().LogGeneracionDocumento.Where(log => log.Estado == estado).ToListAsync() ?? new List<LogGeneracionDocumento>();
        }

        public async Task ResetLogs()
        {
            try
            {
                CreditoWebContext context = _contextFactory.CreateDbContext();
                await context.LogGeneracionDocumento.ForEachAsync(i => { i.Estado = false; i.FechaProcesoFin = null; i.Error = null; });
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteLogs()
        {
            try
            {
                CreditoWebContext context = _contextFactory.CreateDbContext();
                await context.LogGeneracionDocumento.ExecuteDeleteAsync();
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LogGeneracionDocumento> GetLogGeneracionWithStatusZeroByIdTramite(int idTramite)
        {
            return await _contextFactory.CreateDbContext().LogGeneracionDocumento.Where(log => log.IdTramite == idTramite && log.Estado == false).FirstOrDefaultAsync() ?? new LogGeneracionDocumento();
        }
    }
}
