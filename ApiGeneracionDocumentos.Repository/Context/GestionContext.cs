using Microsoft.EntityFrameworkCore;

namespace ApiGeneracionDocumentos.Repository.Context
{
    public partial class GestionContext : DbContext
    {
        public GestionContext(DbContextOptions<GestionContext> options)
            : base(options) { }

        public GestionContext()
            : base() { }
    }
}
