using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Entity;
using ApiGeneracionDocumentos.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiGeneracionDocumentos.Repository.Services
{
    public class ConfiguracionRepository : IConfiguracionRepository
    {
        private readonly IDbContextFactory<ConfiguracionContext> _context;
        public ConfiguracionRepository(IDbContextFactory<ConfiguracionContext> context) 
        {
            _context = context;
        }

        public async Task<Parametro> GetParametroByNombre(string nombre)
        {
            return await _context.CreateDbContext().Parametro.Where(parametro => parametro.Nombre == nombre).SingleOrDefaultAsync() ?? new Parametro();
        }
    }
}
