using ApiGeneracionDocumentos.Entity;

namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface IConfiguracionInfraestructure
    {
        Task<Parametro> GetParametroByNombre(string nombre);
    }
}