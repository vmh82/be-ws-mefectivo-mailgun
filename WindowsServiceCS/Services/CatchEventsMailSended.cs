using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using LogMailGunSvc.Entities;
using LogMailGunSvc.Utils;
using System.Collections.Generic;
using System;
using System.Configuration;

namespace LogMailGunSvc.Services
{
    public class CatchEventsMailSended
    {

        private List<Item> _list = new List<Item>();
        private string _nextPage = string.Empty;

        public List<MensajeLog> GetLogEntry(MensajeLog registroMasActual, string fechaInicio, string fechaFin = null)
        {

            List<MensajeLog> correos = new List<MensajeLog>();

            var domain = ConfigurationManager.AppSettings["Domain"].ToString();
            var apiKey = ConfigurationManager.AppSettings["ApiKey"].ToString();

            RestClient client = new RestClient("https://api.mailgun.net/v3")
            {
                Authenticator = new HttpBasicAuthenticator("api", apiKey)
            };
            RestRequest request = CrearRequest(domain, fechaInicio, fechaFin);
            var response = client.Execute(request);

            RootMGRC rootMGRC = JsonConvert.DeserializeObject<RootMGRC>(response.Content);

            if (rootMGRC.items.Count <= 0) LogUtil.Logger("No se encontró registros en MailGun");

            _list = rootMGRC.items;
            _nextPage = rootMGRC.paging.next;

            while (_list.Count > 0)
            {

                foreach (var item in _list)
                {
                    MensajeLog _correo = new MensajeLog
                    {
                        Timestamp = item.timestamp.ToString(),
                        FechaHora = FechaUtil.ConvertirAFechaHora(item.timestamp),
                        Evento = item.@event,
                        FromEmail = item.message.headers.from,
                        ToEmail = item.message.headers.to,
                        Id = item.id,
                        Subject = item.message.headers.subject,
                        JsonBody = JsonConvert.SerializeObject(item, Formatting.None)
                    };
                    correos.Add(_correo);
                }
                NextPag(_nextPage, apiKey);
            }
            if (registroMasActual != null)
            {
                var existePorId = correos.Find(x => x.Id == registroMasActual.Id);

                correos.Remove(existePorId);
            }

            LogUtil.Logger($"Total # = {correos.Count}");

            return correos;
        }


        public RootMGRC NextPag(string urlNext, string apiKey)
        {
            if (string.IsNullOrEmpty(urlNext)) throw new Exception($"API MailGun no retorno urlNext");

            RestClient clientNextPage = new RestClient(urlNext)
            {
                Authenticator =
                    new HttpBasicAuthenticator("api", apiKey)
            };

            RestRequest requestNextPage = new RestRequest()
            {
                Method = Method.Get
            };

            var responseNextPage = clientNextPage.Execute(requestNextPage);
            RootMGRC rootMGRCNextPage = JsonConvert.DeserializeObject<RootMGRC>(responseNextPage.Content);
            _list = rootMGRCNextPage.items;
            _nextPage = rootMGRCNextPage.paging.next;

            return rootMGRCNextPage;
        }

        private static RestRequest CrearRequest(string domain, string fechaInicio, string fechaFin)
        {
            RestRequest request = new RestRequest($"/{domain}/events", Method.Get);

            request.AddParameter("begin", fechaInicio);
            if (!string.IsNullOrEmpty(fechaFin))
            {
                request.AddParameter("end", fechaFin);
            }
            request.AddParameter("ascending", "yes");

            return request;
        }
    }
}
