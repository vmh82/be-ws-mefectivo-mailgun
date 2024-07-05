using ApiGeneracionDocumentos.Entity;

namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface IConfiguracionRepository
    {
        Task<Parametro> GetParametroByNombre(string nombre);
    }
}