using ApiGeneracionDocumentos.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiGeneracionDocumentos.Repository.Context
{
    public partial class FormularioContext : DbContext
    {
        public virtual DbSet<FormularioVersion> FormularioVersion { get; set; }
        public virtual DbSet<Formulario> Formulario { get; set; }

        public FormularioContext(DbContextOptions<FormularioContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FormularioVersion>(entity => 
            {
                entity.HasKey(e => e.IdFormularioVersion);
                entity.ToTable("FormularioVersion");
            });

            modelBuilder.Entity<Formulario>(entity =>
            {
                entity.HasKey(e => e.IdFormulario);
                entity.ToTable("Formulario");
            });
        }
    }
}
