using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Entity.Dto;

namespace ApiGeneracionDocumentos.Infraestructure.Services
{
    public class PictoreInfraestructure : IPictoreInfraestructure
    {
        private readonly IPictoreRepository _pictoreRepository;
        public PictoreInfraestructure(IPictoreRepository pictoreRepository)
        {
            _pictoreRepository = pictoreRepository;
        }

        public List<DtoRutasPictore> GetRutasPictore(string identificacion, int? lotePictore, int idTramite)
        {
            return _pictoreRepository.GetRoutesPictorByIdClienteAndLotePictoreAndIdTramiteAsync(identificacion, lotePictore, idTramite);
        }
    }
}
