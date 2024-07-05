using ApiGeneracionDocumentos.Entity;
using System.Data;

namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface IDocumentoRepository
    {
        DataSet GetInformationByProcedure(string codigoModulo, string procedimiento, string numeroSolicitud);
        DataSet CentralDocumentGenerator();
        Task<Documento> GetDocumentoByCodigo(string codigo);
    }
}