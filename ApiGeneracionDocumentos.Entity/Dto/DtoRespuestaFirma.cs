using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ApiGeneracionDocumentos.Entity.Dto
{
    public class DtoRespuestaFirma
    {
        public string? descripcion { get; set; }
        public string? documento { get; set; }
        public firma firmaTitular { get; set; }
        public firma firmaBanco { get; set; }
    }

    public class firma
    {
        public string? paginas { get; set; }
        public string? tamanioFuente { get; set; }
    }
}
