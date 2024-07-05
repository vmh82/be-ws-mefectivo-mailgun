using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Entity;
using ApiGeneracionDocumentos.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiGeneracionDocumentos.Repository.Services
{
    public class ReconocimientoFacialRepository : IReconocimientoFacialRepository
    {
        private readonly IDbContextFactory<CreditoWebContext> _context;
        public ReconocimientoFacialRepository(IDbContextFactory<CreditoWebContext> context) 
        {
            _context = context;
        }

        public async Task<ReconocimientoFacial> GetReconocimientoFacialByNumeroTramite(string numeroTramite)
        {
            return await _context.CreateDbContext().ReconocimientoFacial.Where(reconocimiento => reconocimiento.NumeroTramite == numeroTramite).SingleOrDefaultAsync() ?? new ReconocimientoFacial();
        }
    }
}
