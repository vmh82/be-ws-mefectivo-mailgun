using System.Data;

namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface ICampaniaRepository
    {
        DataSet GetNombreCampaniaByCodigoCampania(string codigoCampania);
    }
}