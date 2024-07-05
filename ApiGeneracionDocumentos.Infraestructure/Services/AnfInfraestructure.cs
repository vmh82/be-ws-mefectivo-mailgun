using ApiGeneracionDocumentos.Domain.Services;
using ApiGeneracionDocumentos.Entity.Dto;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGeneracionDocumentos.Infraestructure.Services
{
    public class AnfInfraestructure : IAnfInfraestructure
    {
        private readonly IAnfRepository _anfRepository;

        public AnfInfraestructure(IAnfRepository anfRepository)
        {
            _anfRepository = anfRepository;
        }

        public Task RecuperarDocumentosFirmados(DtoRequest dtoRequest)
        {
            return _anfRepository.RecuperarDocumentosFirmados(dtoRequest);
        }
    }
}
