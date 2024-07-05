using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogMailGunSvc.Entities
{
    public class MensajeLog
    {
        public string Id { get; set; }
        public string Evento { get; set; }
        public string Timestamp { get; set; }
        public DateTime FechaHora { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string JsonBody { get; set; }
    }

}

