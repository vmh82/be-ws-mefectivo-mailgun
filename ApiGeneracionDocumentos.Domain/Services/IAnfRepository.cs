using ApiGeneracionDocumentos.Entity.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGeneracionDocumentos.Domain.Services
{
    public interface IAnfRepository
    {
       Task RecuperarDocumentosFirmados(DtoRequest dtoRequest);
    }
}
