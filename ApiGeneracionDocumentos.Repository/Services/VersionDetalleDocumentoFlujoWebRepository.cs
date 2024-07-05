using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Entity;
using ApiGeneracionDocumentos.Entity.Dto;
using ApiGeneracionDocumentos.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiGeneracionDocumentos.Repository.Services
{
    public class VersionDetalleDocumentoFlujoWebRepository : IVersionDetalleDocumentoFlujoWebRepository
    {
        private readonly IDbContextFactory<CreditoWebContext> _context;
        private readonly IDbContextFactory<FormularioContext> _contextFormulario;
        public VersionDetalleDocumentoFlujoWebRepository(IDbContextFactory<CreditoWebContext> context, IDbContextFactory<FormularioContext> contextFormulario)
        {
            _context = context;
            _contextFormulario = contextFormulario;
        }

        public async Task<List<DtoDocumentoFlujoWeb>> GetDetalleVersionesByIdVersionDocumento(int idVersionDocumentoFlujoWeb, DateTime fechaDesembolso)
        {
            try
            {
                List<DtoDocumentoFlujoWeb> result = new();
                CreditoWebContext creditoWebContext = _context.CreateDbContext();
                FormularioContext formularioContext = _contextFormulario.CreateDbContext();

                List<VersionDetalleDocumentoFlujoWeb> versionesDetalle = await creditoWebContext.VersionDetalleDocumentoFlujoWeb.Where(i => i.IdVersionDocumentoFlujoWeb == idVersionDocumentoFlujoWeb).ToListAsync();
                IEnumerable<int> idDocuments = versionesDetalle.OrderBy(a => a.OrdenInicial).Select(i => i.IdDocumento);

                List<Documento> documents = await creditoWebContext.Documento.Where(i => idDocuments.Contains(i.IdDocumento)).ToListAsync();
                IEnumerable<string> codigoDocumentos = documents.Select(i => i.Codigo);

                List<Formulario> formularios = await formularioContext.Formulario.Where(a => codigoDocumentos.Contains(a.CodigoFormulario)).ToListAsync();
                IEnumerable<int> idFormularios = formularios.Select(i => i.IdFormulario);

                List<FormularioVersion> formulariosVersion = await formularioContext.FormularioVersion.Where(f => idFormularios.Contains(f.IdFormulario)).OrderBy(i=>i.FechaVigencia).ToListAsync();

                foreach (var version in versionesDetalle)
                {
                    FormularioVersion formularioVersion = new();
                    Documento documento = documents.Find(i => i.IdDocumento == version.IdDocumento)!;
                    if(documento.Codigo != "WEBTOTAL")
                    {
                        int idFormulario = formularios.Find(i => i.CodigoFormulario == documento.Codigo)!.IdFormulario;
                        List<FormularioVersion> formularioVersiones = formulariosVersion.FindAll(i => i.IdFormulario == idFormulario);
                        formularioVersion = GetFormularioVersionByNearestDate(fechaDesembolso, formularioVersiones);
                    }

                    result.Add(new DtoDocumentoFlujoWeb()
                    {
                        IdDocumento = version.IdDocumento,
                        Codigo = documento.Codigo,
                        Modulo = documento.Modulo,
                        NombreDocumento = documento.NombreDocumento,
                        Procedimiento = documento.Procedimiento,
                        ObjetoFormato = formularioVersion?.ObjetoFormato ?? Array.Empty<byte>()
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private static FormularioVersion GetFormularioVersionByNearestDate(DateTime fechaDesembolso, List<FormularioVersion> formularioVersiones)
        {
            FormularioVersion result = new();
            foreach (FormularioVersion version in formularioVersiones)
            {
                if (fechaDesembolso.Date >= version.FechaVigencia.Date)
                {
                    result = version;
                }
            }
            return result.IdFormularioVersion == 0 ? formularioVersiones.First() : result;
        }

    }
}
