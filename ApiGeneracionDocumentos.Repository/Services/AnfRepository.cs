using ApiGeneracionDocumentos.Domain.Services;
using ApiGeneracionDocumentos.Entity.Constants;
using ApiGeneracionDocumentos.Entity.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ApiGeneracionDocumentos.Repository.Services
{
    public class AnfRepository : IAnfRepository
    {
        private readonly IConfiguration _configuration;
        public AnfRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task RecuperarDocumentosFirmados(DtoRequest dtoRequest)
        {
            DtoResponse response = new DtoResponse();
            try
            {               
                List<Tuple<string, byte[]>> documentosFirmados = new List<Tuple<string, byte[]>>();
                string tokenSeguridad = GenerarToken().Result;
                documentosFirmados = RecuperarDocumentosFirmados(tokenSeguridad, dtoRequest.IdFirma).Result;
                foreach (var documentoFirmado in documentosFirmados)
                {
                    var urlRutaDocumento = Path.Combine(_configuration.GetSection(ComunicationConstant.RutaDriveDocumentos).Value, _configuration.GetSection(ComunicationConstant.RutaDocumentos).Value); 
                    if (!string.IsNullOrEmpty(urlRutaDocumento))
                        File.WriteAllBytes(Path.Combine(urlRutaDocumento, dtoRequest.IdFirma + ".pdf"), documentoFirmado.Item2);                    
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<List<Tuple<string, byte[]>>> RecuperarDocumentosFirmados(string tokenSeguridad, string? tokenRespuesta)
        {
            List<Tuple<string, byte[]>> resultado = new List<Tuple<string, byte[]>>();
            try
            {
                string respuesta = string.Empty;
                string? url = _configuration.GetSection(ComunicationConstant.FDUrlFirmas).Value;
                if (url == string.Empty || url == null)
                    throw new Exception(ExceptionConstant.ParametrizacionFDUrlAuth);

                string? urlDocumentoFirmado = _configuration.GetSection(ComunicationConstant.FDUrlObtenerDocumentoFirmado).Value;

                if (urlDocumentoFirmado == string.Empty || urlDocumentoFirmado == null)
                    throw new Exception(ExceptionConstant.ParametrizacionFDUrlObtenerDocumentoFirmado);

                var urlFinal = url + "/" + tokenRespuesta + "/" + urlDocumentoFirmado;
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(10);
                    var request = new HttpRequestMessage
                    {
                        RequestUri = new Uri(urlFinal),
                        Method = HttpMethod.Get
                    };

                    request.Headers.Add("Authorization", tokenSeguridad);
                    var documento = await client.SendAsync(request);
                    respuesta = await documento.Content.ReadAsStringAsync();
                    DtoError error = TryParseJson<DtoError>(respuesta);
                    if (error == null)
                    {
                        List<DtoRespuestaFirma> documentosRespuesta = TryParseJson<List<DtoRespuestaFirma>>(respuesta);
                        if (documentosRespuesta != null)
                        {
                            foreach (var documentoRespuesta in documentosRespuesta)
                            {
                                resultado.Add(new Tuple<string, byte[]>(documentoRespuesta.descripcion, Convert.FromBase64String(documentoRespuesta.documento)));
                            }
                        }
                        else
                        {
                            string mensaje = TryParseJson<string>(respuesta);
                            if (mensaje == null)
                                throw new Exception(ExceptionConstant.ErrorDocumentosFirmados);
                        }
                    }
                    else
                    {
                        throw new Exception(respuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }


        private async Task<string> GenerarToken()
        {
            string tokenValor = string.Empty;
            try
            {                
                DtoTokenFirma dtoToken = new DtoTokenFirma();
                dtoToken.id = _configuration.GetSection(ComunicationConstant.FDUsuario).Value;
                dtoToken.password = _configuration.GetSection(ComunicationConstant.FDClave).Value;
                string? url = _configuration.GetSection(ComunicationConstant.FDUrlAuth).Value;
                
                if (dtoToken.id == string.Empty || dtoToken.id == null)
                    throw new Exception(ExceptionConstant.ParametrizacionFDUsuario);

                if (dtoToken.password == string.Empty || dtoToken.password == null)
                    throw new Exception(ExceptionConstant.ParametrizacionFDClave);

                if (url == string.Empty || url == null)
                    throw new Exception(ExceptionConstant.ParametrizacionFDUrlAuth);

                string json = JsonConvert.SerializeObject(dtoToken);
                var data = new StringContent(json, Encoding.UTF8, "application/json");


                using (var cliente = new HttpClient())
                {
                    cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await cliente.PostAsync(url, data).ConfigureAwait(false);
                    var token = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    DtoError error = TryParseJson<DtoError>(token);
                    if (error == null)
                    {
                        tokenValor = TryParseJson<string>(token);
                        if (string.IsNullOrEmpty(tokenValor))
                            throw new Exception(ExceptionConstant.ErrorTokenSeguridad);
                    }
                    else
                    {
                        throw new Exception(token);
                    }

                }
            }
            catch (Exception ex)
            {
                 throw  ex;
            }

            return tokenValor;
        }
        private T TryParseJson<T>(string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return default(T);
            }

            var value = stringValue.Trim();

            if ((value.StartsWith("{") && value.EndsWith("}")) || (value.StartsWith("[") && value.EndsWith("]")) || (value.StartsWith("\"") && value.EndsWith("\"")))
            {
                try
                {
                    var obj = JsonConvert.DeserializeObject<T>(value);
                    return obj;
                }
                catch (Exception)
                {
                    return default(T);
                }
            }

            return default(T);
        }
    }
}
