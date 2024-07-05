using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Entity;
using ApiGeneracionDocumentos.Entity.Dto;
using System.Xml;

namespace ApiGeneracionDocumentos.Infraestructure.Services
{
    public class TramiteInfraestructure : ITramiteInfraestructure
    {
        private readonly IClienteInfraestructure _clienteInfraestructure;
        private readonly ITramiteRepository _tramiteRepository;

        public TramiteInfraestructure(ITramiteRepository tramiteRepository, IClienteInfraestructure clienteInfraestructure)
        {
            _tramiteRepository = tramiteRepository;
            _clienteInfraestructure = clienteInfraestructure;
        }

        public async Task<Tramite> GetTramiteByIdTramite(int idtramite)
        {
            return await _tramiteRepository.GetTramiteByIdTramite(idtramite);
        }

        public async Task<DtoTramite> GetDtoTramiteByIdTramite(int idtramite)
        {
            Tramite tramite = await _tramiteRepository.GetTramiteByIdTramite(idtramite);
            return await MapperFromTramiteToDtoTramite(tramite);
        }

        public async Task<DtoTramite> GetTramiteByIdentificacionAndFlujoWebAndFechaDesem(string identificacion, string codigoFlujoWeb, DateTime fechaDesembolso)
        {
            try
            {
                Cliente cliente = await _clienteInfraestructure.GetClienteByIdentificacion(identificacion);
                List<Tramite> tramites = await _tramiteRepository.GetTramitesByIdentificacionAndFlujoWeb(cliente.IdCliente, codigoFlujoWeb);
                Tramite tramite = tramites.Where(tramite => tramite.FechaDesembolso.ToString("yyyy/MM/dd") == fechaDesembolso.ToString("yyyy/MM/dd")).FirstOrDefault() ?? new Tramite();                
                return await MapperFromTramiteToDtoTramite(tramite);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Tramite> GerTramiteByNroSolicitud(string nroSolicitud)
        {
            return await _tramiteRepository.GerTramiteByNroSolicitud(nroSolicitud);
        }

        public async Task<List<DtoTramite>> GetTramitesByDateRange(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Tramite> tramites = await _tramiteRepository.GetTramitesByDateRange(fechaInicio, fechaFin);
            List<DtoTramite> dtoTramites = new();
            foreach (Tramite tramite in tramites)
            {
                dtoTramites.Add(await MapperFromTramiteToDtoTramite(tramite));
            }
            return dtoTramites;
        }

        private async Task<DtoTramite> MapperFromTramiteToDtoTramite(Tramite tramite)
        {
            Cliente cliente = await _clienteInfraestructure.GetClienteByIdCliente(tramite.IdCliente);
            string codigoCampania = GetCodigoCampaniaFromXML(tramite.XmlGeneracionTabla!);
            return new()
            {
                Identificacion = cliente.Identificacion,
                IdTramite = tramite.IdTramite,
                IdCliente = tramite.IdCliente,
                FechaDesembolso = tramite.FechaDesembolso,
                IdFlujoWeb = tramite.IdFlujoWeb,
                LotePictor = tramite.LotePictor,
                NroSolicitud = tramite.NroSolicitud,
                NumeroTramite = tramite.NumeroTramite,
                SeCreoCuenta = tramite.SeCreoCuenta,
                ValorSeguro = tramite.ValorSeguro,
                CodigoCampania = codigoCampania
            };
        }

        private static string GetCodigoCampaniaFromXML(string xml)
        {
            string codigoCampania = string.Empty;
            XmlDocument xmlDoc = new();
            xmlDoc.LoadXml(xml);
            XmlNodeList nodes = xmlDoc.SelectNodes("SOLICITUD/REGLAS/REGLA")!;
            foreach (XmlNode node in nodes)
            {
                XmlAttributeCollection nodeAtt = node.Attributes!;
                codigoCampania = nodeAtt["CodigoReglaCampania"]?.Value!;
            }
            return codigoCampania;
        }
    }
}
