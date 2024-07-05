using ApiGeneracionDocumentos.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiGeneracionDocumentos.Repository.Context
{
    public partial class ConfiguracionContext : DbContext
    {
        public virtual DbSet<Parametro> Parametro { get; set; }
        public ConfiguracionContext(DbContextOptions<ConfiguracionContext> options)
            : base(options) { }

        public ConfiguracionContext()
            : base() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parametro>(entity =>
            {
                entity.HasKey(e => e.IdParametro);
                entity.ToTable("Parametro");
            });
        }
    }
}
