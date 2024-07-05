using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Entity.Dto;
using ApiGeneracionDocumentos.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiGeneracionDocumentos.Repository.Services
{
    public class PictoreRepository : IPictoreRepository
    {
        private readonly IDbContextFactory<PictoreContext> _pictoreContext;
        public PictoreRepository(IDbContextFactory<PictoreContext> pictoreContext)
        {
            _pictoreContext = pictoreContext;
        }

        public List<DtoRutasPictore> GetRoutesPictorByIdClienteAndLotePictoreAndIdTramiteAsync(string identificacion, int? lotePictore, int idTramite)
        {
            PictoreContext pictoreContext = _pictoreContext.CreateDbContext();
            List<DtoRutasPictore> result = new();
            var list = pictoreContext.CarpetasClaves
                .Join(
                    pictoreContext.ImagenesCarpeta,
                    cc => cc.CARP_CODIGO,
                    ic => ic.CARP_CODIGO,
                    (cc, ic) => new { ic, cc })
                .Join(
                    pictoreContext.Imagenes,
                    ic => ic.ic.IMAG_CODIGO,
                    img => img.IMAG_CODIGO,
                    (ic, img) => new { img, ic.cc })
                .Join(
                    pictoreContext.RutaDisco,
                    img => img.img.RUDI_CODIGO,
                    rut => rut.RUDI_CODIGO,
                    (img, rut) => new { img.img.IMAG_RUTA, img.img.IMAG_NOMBRE, rut, img.cc })
                .Join(
                    pictoreContext.Ruta,
                    rut => rut.rut.RUTA_CODIGO,
                    rutas => rutas.RUTA_CODIGO,
                    (rut, rutas) => new { rut.IMAG_RUTA, rutas.RUTA_PATH, rut.IMAG_NOMBRE, rut.cc })
                .Where(
                    aux => aux.cc.CLIM_VALOR == identificacion
                    && aux.IMAG_RUTA.Contains("MP" + lotePictore)
                    && aux.cc.CREQ_CODIGO == 245)
                .ToList();

            list.ForEach(aux => result.Add(
                new DtoRutasPictore()
                {
                    IdTramite = idTramite,
                    RutaLocal = aux.IMAG_RUTA,
                    RutaCentral = aux.RUTA_PATH,
                    NombreDocumento = aux.IMAG_NOMBRE
                })
            );

            return result;
        }
    }
}
