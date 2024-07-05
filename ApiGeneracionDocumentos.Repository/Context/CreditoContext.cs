using Microsoft.EntityFrameworkCore;

namespace ApiGeneracionDocumentos.Repository.Context
{
    public partial class CreditoContext : DbContext
    {
        public CreditoContext(DbContextOptions<CreditoContext> options)
            : base(options) { }

        public CreditoContext()
            : base() { }
    }
}
