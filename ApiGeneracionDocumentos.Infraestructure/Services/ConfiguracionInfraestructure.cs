using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Entity;

namespace ApiGeneracionDocumentos.Infraestructure.Services
{
    public class ConfiguracionInfraestructure : IConfiguracionInfraestructure
    {
        private readonly IConfiguracionRepository _configuracionRepository;
        public ConfiguracionInfraestructure(IConfiguracionRepository configuracionRepository)
        {
            _configuracionRepository = configuracionRepository;
        }

        public async Task<Parametro> GetParametroByNombre(string nombre)
        {
            return await _configuracionRepository.GetParametroByNombre(nombre);
        }
    }
}
