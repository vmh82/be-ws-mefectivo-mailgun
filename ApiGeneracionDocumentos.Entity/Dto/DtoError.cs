using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiGeneracionDocumentos.Entity.Dto
{
    public class DtoError
    {
        public string? code { get; set; }
        public string? message { get; set; }
        public string? typeCode { get; set; }
        public List<errores> errores { get; set; }
    }

    public class errores
    {
        public string? tipo { get; set; }
        public string? descripcion { get; set; }
    }
}
