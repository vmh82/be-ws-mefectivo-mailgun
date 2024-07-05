using ApiGeneracionDocumentos.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiGeneracionDocumentos.Repository.Context
{
    public partial class CreditoWebContext : DbContext
    {
        public virtual DbSet<Tramite> Tramite { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<LogGeneracionDocumento> LogGeneracionDocumento { get; set; }
        public virtual DbSet<VersionDocumentoFlujoWeb> VersionDocumentoFlujoWeb { get; set; }
        public virtual DbSet<VersionDetalleDocumentoFlujoWeb> VersionDetalleDocumentoFlujoWeb { get; set; }
        public virtual DbSet<Documento> Documento { get; set; }
        public virtual DbSet<FlujoWeb> FlujoWeb { get; set; }
        public virtual DbSet<ReconocimientoFacial> ReconocimientoFacial { get; set; }

        public CreditoWebContext(DbContextOptions<CreditoWebContext> options)
            : base(options) { }

        public CreditoWebContext()
            : base() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tramite>(entity => 
            {
                entity.HasKey(e => e.IdTramite);
                entity.ToTable("Tramite");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);
                entity.ToTable("Cliente");
            });

            modelBuilder.Entity<LogGeneracionDocumento>(entity =>
            {
                entity.HasKey(e => e.IdLogGeneracionDocumento);
                entity.ToTable("LogGeneracionDocumento");
            });

            modelBuilder.Entity<VersionDocumentoFlujoWeb>(entity =>
            {
                entity.HasKey(e => e.IdVersionDocumentoFlujoWeb);
                entity.ToTable("VersionDocumentoFlujoWeb");
            });

            modelBuilder.Entity<VersionDetalleDocumentoFlujoWeb>(entity =>
            {
                entity.HasKey(e => e.IdVersionDetalleDocumentoFlujoWeb);
                entity.ToTable("VersionDetalleDocumentoFlujoWeb");
            });

            modelBuilder.Entity<Documento>(entity =>
            {
                entity.HasKey(e => e.IdDocumento);
                entity.ToTable("Documento");
            });

            modelBuilder.Entity<FlujoWeb>(entity =>
            {
                entity.HasKey(e => e.IdFlujoWeb);
                entity.ToTable("FlujoWeb");
            });

            modelBuilder.Entity<ReconocimientoFacial>(entity =>
            {
                entity.HasKey(e => e.IdReconocimientoFacial);
                entity.ToTable("ReconocimientoFacial");
            });
        }
    }
}
