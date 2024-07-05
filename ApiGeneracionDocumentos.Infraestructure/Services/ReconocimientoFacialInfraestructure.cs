using ApiGeneracionDocumentos.Domain.Interfaces;

namespace ApiGeneracionDocumentos.Infraestructure.Services
{
    public class ReconocimientoFacialInfraestructure : IReconocimientoFacialInfraestructure
    {
        private readonly IReconocimientoFacialRepository _reconomientoFacialRepository;
        public ReconocimientoFacialInfraestructure(IReconocimientoFacialRepository reconomientoFacialRepository)
        {
            _reconomientoFacialRepository = reconomientoFacialRepository;
        }

        public string GetIdFirmaByNumeroTramite(string numeroTramite)
        {
            string message = _reconomientoFacialRepository.GetReconocimientoFacialByNumeroTramite(numeroTramite).Result.Mensaje;
            return message.Split("-")[0].Split(":")[1].Trim();
        }
    }
}
