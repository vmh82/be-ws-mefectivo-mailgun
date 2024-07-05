using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Entity;
using ApiGeneracionDocumentos.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiGeneracionDocumentos.Repository.Services
{
    public class VersionDocumentoFlujoWebRepository : IVersionDocumentoFlujoWebRepository
    {
        private readonly IDbContextFactory<CreditoWebContext> _context;
        public VersionDocumentoFlujoWebRepository(IDbContextFactory<CreditoWebContext> context) 
        {
            _context = context;
        }

        public async Task<List<VersionDocumentoFlujoWeb>> GetVersionesByIdFlujoWebOrderByFechaVigencia(int idFlujoWeb)
        {
            return await _context.CreateDbContext().VersionDocumentoFlujoWeb.Where(version => version.IdFlujoWeb == idFlujoWeb).OrderBy(item => item.FechaVigencia).ToListAsync();
        }
    }
}
