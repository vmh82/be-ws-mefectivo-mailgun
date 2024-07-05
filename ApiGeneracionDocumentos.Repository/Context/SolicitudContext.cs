using Microsoft.EntityFrameworkCore;

namespace ApiGeneracionDocumentos.Repository.Context
{
    public partial class SolicitudContext : DbContext
    {
        public SolicitudContext(DbContextOptions<SolicitudContext> options)
            : base(options) { }

        public SolicitudContext()
            : base() { }
    }
}
