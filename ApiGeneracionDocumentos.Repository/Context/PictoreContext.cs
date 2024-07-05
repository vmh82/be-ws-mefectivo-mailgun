using ApiGeneracionDocumentos.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiGeneracionDocumentos.Repository.Context
{
    public partial class PictoreContext : DbContext
    {
        public virtual DbSet<CarpetasClaves> CarpetasClaves { get; set; }
        public virtual DbSet<ImagenesCarpeta> ImagenesCarpeta { get; set; }
        public virtual DbSet<Imagenes> Imagenes { get; set; }
        public virtual DbSet<RutaDisco> RutaDisco { get; set; }
        public virtual DbSet<Ruta> Ruta { get; set; }

        public PictoreContext(DbContextOptions<PictoreContext> options)
            : base(options) { }

        public PictoreContext()
            : base() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarpetasClaves>(entity =>
            {
                entity.HasKey(e => e.CACL_CODIGO);
                entity.ToTable("CARPETA_CLAVES");
            });

            modelBuilder.Entity<ImagenesCarpeta>(entity =>
            {
                entity.HasKey(e => e.IMCA_CODIGO);
                entity.ToTable("IMAGENES_CARPETA");
            });

            modelBuilder.Entity<Imagenes>(entity =>
            {
                entity.HasKey(e => e.IMAG_CODIGO);
                entity.ToTable("IMAGENES");
            });

            modelBuilder.Entity<RutaDisco>(entity =>
            {
                entity.HasKey(e => e.RUDI_CODIGO);
                entity.ToTable("RUTA_DISCO");
            });

            modelBuilder.Entity<Ruta>(entity =>
            {
                entity.HasKey(e => e.RUTA_CODIGO);
                entity.ToTable("RUTAS");
            });
        }
    }
}
