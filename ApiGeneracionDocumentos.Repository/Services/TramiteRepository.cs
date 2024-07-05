using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Entity;
using ApiGeneracionDocumentos.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiGeneracionDocumentos.Repository.Services
{
    public class TramiteRepository : ITramiteRepository
    {
        private readonly IDbContextFactory<CreditoWebContext> _context;
        public TramiteRepository(IDbContextFactory<CreditoWebContext> context)
        {
            _context = context;
        }

        public async Task<Tramite> GerTramiteByNroSolicitud(string nroSolicitud)
        {
            return await _context.CreateDbContext().Tramite.Where(tramite => tramite.NroSolicitud == nroSolicitud).SingleOrDefaultAsync() ?? new Tramite();
        }

        public async Task<Tramite> GetTramiteByIdTramite(int idTramite)
        {
            return await _context.CreateDbContext().Tramite.Where(tramite => tramite.IdTramite == idTramite).SingleOrDefaultAsync() ?? new Tramite();
        }

        public async Task<List<Tramite>> GetTramitesByDateRange(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.CreateDbContext().Tramite.Where(tramite => tramite.FechaDesembolso.Date >= fechaInicio.Date && tramite.FechaDesembolso.Date <= fechaFin.Date && (tramite.IdFlujoWeb == 3 || tramite.IdFlujoWeb == 4) && tramite.Estado == "DES").ToListAsync();
        }

        public async Task<List<Tramite>> GetTramitesByIdentificacionAndFlujoWeb(int idCliente, string codigoFlujoWeb)
        {
            CreditoWebContext context = _context.CreateDbContext();
            FlujoWeb flujoWeb = await context.FlujoWeb.Where(flujo => flujo.Codigo == codigoFlujoWeb).FirstOrDefaultAsync() ?? new FlujoWeb();
            return await context.Tramite.Where(tramite => tramite.IdFlujoWeb == flujoWeb.IdFlujoWeb && tramite.IdCliente == idCliente && tramite.Estado == "DES").ToListAsync();
        }
    }
}
