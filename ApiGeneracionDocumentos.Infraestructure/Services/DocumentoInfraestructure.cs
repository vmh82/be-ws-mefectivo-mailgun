using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Domain.Services;
using ApiGeneracionDocumentos.Entity;
using ApiGeneracionDocumentos.Entity.Constants;
using ApiGeneracionDocumentos.Entity.Dto;
using ApiGeneracionDocumentos.Infraestructure.Utils;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;

namespace ApiGeneracionDocumentos.Infraestructure.Services
{
    public class DocumentoInfraestructure : IDocumentoInfraestructure
    {
        private readonly IClienteInfraestructure _clienteInfraestructure;
        private readonly ILogGeneracionDocumentoInfraestructure _logGeneracionDocumentoInfraestructure;
        private readonly IPictoreInfraestructure _pictoreInfraestructure;
        private readonly IConfiguracionInfraestructure _configuracionInfraestructure;
        private readonly ITramiteInfraestructure _tramiteInfraestructure;
        private readonly ICampaniaInfraestructure _campaniaInfraestructure;
        private readonly IConfiguration _configuration;
        private readonly IVersionDocumentoFlujoWebRepository _versionDocumentoFlujoWebRepository;
        private readonly IVersionDetalleDocumentoFlujoWebRepository _versionDetalleDocumentoFlujoWebRepository;
        private readonly IDocumentoRepository _documentoRepository;
        private readonly IAnfRepository _anfRepository;
        private readonly IReconocimientoFacialInfraestructure _reconocimientoFacialInfraestructure;

        public DocumentoInfraestructure(
            IClienteInfraestructure clienteInfraestructure,
            ILogGeneracionDocumentoInfraestructure logGeneracionDocumentoInfraestructure,
            IPictoreInfraestructure pictoreInfraestructure,
            IConfiguracionInfraestructure configuracionInfraestructure,
            ITramiteInfraestructure tramiteInfraestructure,
            ICampaniaInfraestructure campaniaInfraestructure,
            IConfiguration configuration,
            IDocumentoRepository documentoRepository,
            IVersionDocumentoFlujoWebRepository versionDocumentoFlujoWebRepository,
            IVersionDetalleDocumentoFlujoWebRepository versionDetalleDocumentoFlujoWebRepository,
            IAnfRepository anfRepository,
            IReconocimientoFacialInfraestructure reconocimientoFacialInfraestructure)
        {
            _clienteInfraestructure = clienteInfraestructure;
            _logGeneracionDocumentoInfraestructure = logGeneracionDocumentoInfraestructure;
            _pictoreInfraestructure = pictoreInfraestructure;
            _configuracionInfraestructure = configuracionInfraestructure;
            _tramiteInfraestructure = tramiteInfraestructure;
            _campaniaInfraestructure = campaniaInfraestructure;
            _configuration = configuration;
            _versionDocumentoFlujoWebRepository = versionDocumentoFlujoWebRepository;
            _versionDetalleDocumentoFlujoWebRepository = versionDetalleDocumentoFlujoWebRepository;
            _documentoRepository = documentoRepository;
            _anfRepository = anfRepository;
            _reconocimientoFacialInfraestructure = reconocimientoFacialInfraestructure;
        }
        public async Task<IEnumerable<DtoResponse>> DocumentGenerationByTramites(List<DtoTramite> tramites, bool generarDocumentosFaltantes)
        {
            try
            {
                List<DtoResponse> result = new();
                Parametro parametro = await _configuracionInfraestructure.GetParametroByNombre("RUTADOCDIGITAL");
                var listTask = new List<Task>();

                await InsertLogsByTramites(tramites);

                // _documentoRepository.CentralDocumentGenerator();

                List<LogGeneracionDocumento> logGeneracionDocumentos =
                    generarDocumentosFaltantes ?
                    await _logGeneracionDocumentoInfraestructure.GetLogsGeneracionByEstado(false) :
                    await GetLogsGenerationDocumentWithStatusZeroByTramites(tramites);

                foreach (LogGeneracionDocumento log in logGeneracionDocumentos)
                {
                    Task task = Task.Run(async () =>
                    {
                        DtoTramite dtoTramite = await _tramiteInfraestructure.GetDtoTramiteByIdTramite(log.IdTramite);
                        List<DtoRutasPictore> rutasPictore = _pictoreInfraestructure.GetRutasPictore(log.Identificacion!, log.LotePictor, log.IdTramite);
                        DtoInformacionDocumentos dtoInformacionDocumentos = new()
                        {
                            IdFlujoWeb = log.IdFlujoWeb,
                            Identificacion = log.Identificacion,
                            NroSolicitud = log.NroSolicitud,
                            IdTramite = log.IdTramite,
                            LotePictor = log.LotePictor,
                            FechaDesembolso = (DateTime)log.FechaDesembolso!,
                            ValorSeguro = dtoTramite.ValorSeguro,
                            SeCreoCuenta = dtoTramite.SeCreoCuenta,
                            RutasPictore = rutasPictore,
                            RutaDocDigital = parametro.ValorCadena!,
                            CodigoCampania = dtoTramite.CodigoCampania
                        };

                        DtoGeneration dtoGeneracion = await DocumentGeneration(dtoInformacionDocumentos);
                        log.FechaProcesoFin = DateTime.Now;
                        log.Estado = dtoGeneracion.Estado;
                        log.Error = JsonConvert.SerializeObject(dtoGeneracion.Error);
                        await _logGeneracionDocumentoInfraestructure.UpdateLog(log);

                        if (!dtoGeneracion.Estado)
                        {
                            result.Add(new()
                            {
                                IdTramite = dtoGeneracion.IdTramite,
                                ErrorGeneracion = dtoGeneracion.Error.ErrorGeneracionFront,
                                ErrorRutaCentral = dtoGeneracion.Error.ErrorRutaCentralFront
                            });
                        }
                    });
                    listTask.Add(task);

                    if (listTask.Count == 100)
                    {
                        await Task.WhenAll(listTask);
                        listTask.Clear();
                    }
                }

                await Task.WhenAll(listTask);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<DtoDocumentoFlujoWeb>> GetDocumentosToGenerateByFlujoWeb(int idFlujoWeb, DateTime fechaDesembolso)
        {
            try
            {
                List<VersionDocumentoFlujoWeb> versionesDocumentoFlujoWeb = await _versionDocumentoFlujoWebRepository.GetVersionesByIdFlujoWebOrderByFechaVigencia(idFlujoWeb);
                VersionDocumentoFlujoWeb versionDocumentoFlujoWeb = GetVersionByNearestDate(fechaDesembolso, versionesDocumentoFlujoWeb);

                return await _versionDetalleDocumentoFlujoWebRepository.GetDetalleVersionesByIdVersionDocumento(versionDocumentoFlujoWeb.IdVersionDocumentoFlujoWeb, fechaDesembolso);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static LogGeneracionDocumento MapperFromTramiteAndIdentificacion(DtoTramite tramite)
        {
            return new()
            {
                IdTramite = tramite.IdTramite,
                IdCliente = tramite.IdCliente,
                IdFlujoWeb = tramite.IdFlujoWeb,
                NroSolicitud = tramite.NroSolicitud,
                Identificacion = tramite.Identificacion,
                LotePictor = tramite.LotePictor,
                FechaDesembolso = tramite.FechaDesembolso
            };
        }

        private async Task<List<LogGeneracionDocumento>> GetLogsGenerationDocumentWithStatusZeroByTramites(List<DtoTramite> dtoTramites)
        {
            List<LogGeneracionDocumento> result = new();
            for (int i = 0; i < dtoTramites.Count; i++)
            {
                LogGeneracionDocumento log = await _logGeneracionDocumentoInfraestructure.GetLogGeneracionWithStatusZeroByIdTramite(dtoTramites[i].IdTramite);
                if (log.IdLogGeneracionDocumento != 0)
                {
                    result.Add(log);
                }
            }
            return result;
        }

        private async Task InsertLogsByTramites(List<DtoTramite> tramites)
        {
            for (int i = 0; i < tramites.Count; i++)
            {
                LogGeneracionDocumento logGeneracionDocumento = MapperFromTramiteAndIdentificacion(tramites[i]);
                logGeneracionDocumento.FechaProcesoInicio = DateTime.Now;
                await _logGeneracionDocumentoInfraestructure.CreateLog(logGeneracionDocumento);
            };
        }

        private async Task<DtoGeneration> DocumentGeneration(DtoInformacionDocumentos dtoInformacionDocumentos)
        {
            List<DtoErrorGeneracionInterno> dtoErrorGeneracionInternos = new();
            List<DtoErrorRutaCentralInterno> dtoErrorRutaCentralInternos = new();
            string nombreDocumentoTotal = string.Empty;
            try
            {
                List<DtoDocumentoFlujoWeb> dtoDocumentosFlujoWeb = await GetDocumentosToGenerateByFlujoWeb(dtoInformacionDocumentos.IdFlujoWeb, dtoInformacionDocumentos.FechaDesembolso);
                string path = _configuration["RutaDriveDocumentos"] + @"\" + _configuration["RutaDocumentos"];
                RichEditDocumentServer serverTotalDocument = new();
                string rutaLocal = dtoInformacionDocumentos.RutaDocDigital + dtoInformacionDocumentos.LotePictor;

                if (!Directory.Exists(rutaLocal))
                {
                    Directory.CreateDirectory(rutaLocal);
                }

                for (int i = 0; i < dtoDocumentosFlujoWeb.Count; i++)
                {
                    if (dtoDocumentosFlujoWeb[i].ObjetoFormato?.Length != 0)
                    {
                        DtoErrorGeneracionInterno dtoErrorGeneracionInterno = IndividualDocumentGeneration(dtoDocumentosFlujoWeb[i], dtoInformacionDocumentos, path, dtoInformacionDocumentos.RutaDocDigital, serverTotalDocument, i);
                        if (!dtoErrorGeneracionInterno.Estado)
                        {
                            dtoErrorGeneracionInternos.Add(dtoErrorGeneracionInterno);
                        }
                    }
                    else
                    {
                        nombreDocumentoTotal = dtoDocumentosFlujoWeb[i].NombreDocumento;
                    }
                }
                string pathTotalPdf = Path.Combine(dtoInformacionDocumentos.RutaDocDigital + dtoInformacionDocumentos.LotePictor, nombreDocumentoTotal + ".pdf");
                if (!File.Exists(pathTotalPdf))
                {
                    using FileStream pdfFileStream = new(pathTotalPdf, FileMode.Create);
                    serverTotalDocument.ExportToPdf(pdfFileStream);
                    serverTotalDocument.Dispose();
                }

                dtoErrorRutaCentralInternos = CopyLocalFilesToCentralPictor(dtoInformacionDocumentos.RutasPictore);
            }
            catch (Exception ex)
            {
                dtoErrorGeneracionInternos.Add(new()
                {
                    Codigo = nombreDocumentoTotal,
                    Estado = false,
                    Error = ex.Message
                });
            }

            return ReturnDtoGenerationWithValidationIfHasError(dtoInformacionDocumentos.IdTramite, dtoErrorGeneracionInternos, dtoErrorRutaCentralInternos);
        }

        private static DtoGeneration ReturnDtoGenerationWithValidationIfHasError(int idTramite, List<DtoErrorGeneracionInterno> dtoErrorGeneracionInternos, List<DtoErrorRutaCentralInterno> dtoErrorRutaCentralInternos)
        {
            bool state = true;

            List<string> errorsGeneracionFront = new();
            List<string> errorsRutaCentralFront = new();
            dtoErrorGeneracionInternos.ForEach((item) =>
            {
                if (!item.Estado)
                {
                    state = false;
                    errorsGeneracionFront.Add(item.Codigo!);
                }
            });

            dtoErrorRutaCentralInternos.ForEach((item) =>
            {
                if (!item.Estado)
                {
                    state = false;
                    errorsRutaCentralFront.Add(item.Documento!);
                }
            });

            return new()
            {
                IdTramite = idTramite,
                Estado = state,
                Error = new()
                {
                    IdTramite = idTramite,
                    ErrorGeneracion = dtoErrorGeneracionInternos,
                    ErrorRutaCentral = dtoErrorRutaCentralInternos,
                    ErrorGeneracionFront = errorsGeneracionFront,
                    ErrorRutaCentralFront = errorsRutaCentralFront
                }
            };
        }

        private DtoErrorGeneracionInterno IndividualDocumentGeneration(DtoDocumentoFlujoWeb dtoDocumentoFlujoWeb, DtoInformacionDocumentos dtoInformacionDocumentos, string path, string rutaDigital, RichEditDocumentServer server, int index)
        {
            DtoErrorGeneracionInterno dtoErrorGeneracionInterno = new();
            try
            {
                string rutaLocal = rutaDigital + dtoInformacionDocumentos.LotePictor;
                string pathPdf = Path.Combine(rutaLocal, dtoDocumentoFlujoWeb.NombreDocumento + ".pdf");
                if (!File.Exists(pathPdf))
                {

                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    DtoDocumento dtoDocumento = new()
                    {
                        Codigo = dtoDocumentoFlujoWeb.Codigo,
                        NroSolicitud = dtoInformacionDocumentos.NroSolicitud!,
                        FechaDesembolso = dtoInformacionDocumentos.FechaDesembolso,
                        ObjetoFormato = dtoDocumentoFlujoWeb.ObjetoFormato,
                        ValorSeguro = dtoInformacionDocumentos.ValorSeguro,
                        SeCreoCuenta = dtoInformacionDocumentos.SeCreoCuenta,
                        Identificacion = dtoInformacionDocumentos.Identificacion!,
                        CodigoCampania = dtoInformacionDocumentos.CodigoCampania!,
                        PathDoc = Path.Combine(path, dtoDocumentoFlujoWeb.NombreDocumento + dtoInformacionDocumentos.IdTramite + timestamp + ".doc"),
                        PathRtf = Path.Combine(path, dtoDocumentoFlujoWeb.NombreDocumento + dtoInformacionDocumentos.IdTramite + timestamp + ".rtf"),
                        PathPdf = Path.Combine(rutaLocal, dtoDocumentoFlujoWeb.NombreDocumento + ".pdf")
                    };

                    if (HasToBeGenerated(dtoDocumento, dtoInformacionDocumentos))
                    {
                        dtoDocumento.InformacionSP = _documentoRepository.GetInformationByProcedure(dtoDocumentoFlujoWeb.Modulo, dtoDocumentoFlujoWeb.Procedimiento, dtoInformacionDocumentos.NroSolicitud!);
                        GenerarDocumento(dtoDocumento, server, index);
                    }
                }
            }
            catch (Exception ex)
            {
                dtoErrorGeneracionInterno.Codigo = dtoDocumentoFlujoWeb.Codigo;
                dtoErrorGeneracionInterno.Estado = false;
                dtoErrorGeneracionInterno.Error = ex.Message;
            }
            return dtoErrorGeneracionInterno;
        }

        private static List<DtoErrorRutaCentralInterno> CopyLocalFilesToCentralPictor(List<DtoRutasPictore> dtoRutasPictore)
        {
            List<DtoErrorRutaCentralInterno> dtoErrorRutaCentralInternos = new();
            try
            {
                foreach (DtoRutasPictore item in dtoRutasPictore)
                {
                    try
                    {
                        string centralRoute = item.RutaCentral + item.NombreDocumento;
                        if (!Directory.Exists(item.RutaCentral))
                        {
                            Directory.CreateDirectory(item.RutaCentral!);
                        }

                        if (!File.Exists(centralRoute))
                        {
                            File.Copy(item.RutaLocal!, centralRoute);
                        }
                    }
                    catch (Exception ex)
                    {
                        dtoErrorRutaCentralInternos.Add(new()
                        {
                            Documento = item.NombreDocumento,
                            Estado = false,
                            Error = ex.Message
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                dtoErrorRutaCentralInternos.Add(new()
                {
                    Documento = "Method CopyLocalFilesToCentralPictor",
                    Estado = false,
                    Error = ex.Message
                });
            }
            return dtoErrorRutaCentralInternos;
        }

        private static VersionDocumentoFlujoWeb GetVersionByNearestDate(DateTime fechaDesembolso, List<VersionDocumentoFlujoWeb> versionesDocumentoFlujoWeb)
        {
            VersionDocumentoFlujoWeb result = new();
            foreach (VersionDocumentoFlujoWeb version in versionesDocumentoFlujoWeb)
            {
                if (fechaDesembolso.Date >= version.FechaVigencia.Date)
                {
                    result = version;
                }
            }
            return result.IdVersionDocumentoFlujoWeb == 0 ? versionesDocumentoFlujoWeb.First() : result;
        }

        private void GenerarDocumento(DtoDocumento dtoDocumento, RichEditDocumentServer serverTotal, int contador)
        {
            try
            {
                DocumentUtil documentuUtil = new();
                using (FileStream fs = File.Create(dtoDocumento.PathDoc))
                {
                    byte[] arreglo = dtoDocumento.ObjetoFormato;
                    fs.Write(arreglo, 0, arreglo.Length);
                }

                RichEditDocumentServer server = new();
                server.LoadDocument(dtoDocumento.PathDoc, DocumentFormat.OpenXml);
                DataColumn columnaFecha = new("FechaSistemaWeb", typeof(string));

                if (dtoDocumento.InformacionSP.Tables[0].Rows.Count > 1)
                {
                    DataSet dsTablaNueva = new();
                    dsTablaNueva.Merge(dtoDocumento.InformacionSP.Clone());
                    dsTablaNueva.Tables[0].Columns.Add(columnaFecha);

                    DataRow dtrFila = dsTablaNueva.Tables[0].NewRow();
                    dtrFila["FechaSistemaWeb"] = dtoDocumento.FechaDesembolso?.ToString("yyyy/MM/dd");
                    foreach (DataColumn columna in dsTablaNueva.Tables[0].Columns)
                    {
                        dtrFila[columna.ColumnName] = dtoDocumento.InformacionSP.Tables[0].Rows[0][columna.ColumnName];
                    }
                    dsTablaNueva.Tables[0].Rows.Add(dtrFila);
                    server.Options.MailMerge.DataSource = dsTablaNueva.Tables[0];
                }
                else
                {
                    dtoDocumento.InformacionSP.Tables[0].Columns.Add(columnaFecha);
                    for (int i = 0; i < dtoDocumento.InformacionSP.Tables[0].Rows.Count; i++)
                    {
                        dtoDocumento.InformacionSP.Tables[0].Rows[i]["FechaSistemaWeb"] = dtoDocumento.FechaDesembolso?.ToString("yyyy/MM/dd");
                    }
                    // server.Options.MailMerge.DataSource = dtoDocumento.InformacionSP.Tables[0];
                }
                MailMergeOptions myMergeOptions =
                 server.Document.CreateMailMergeOptions();
                myMergeOptions.DataSource = dtoDocumento.InformacionSP.Tables[0];
                myMergeOptions.MergeMode = MergeMode.NewSection;
                server.MailMerge(myMergeOptions, dtoDocumento.PathRtf, DocumentFormat.Rtf);
                server.LoadDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);

                switch (dtoDocumento.Codigo)
                {
                    #region MicroEmpresa
                    case "WEBINSTRUCCION":
                        documentuUtil.GenerarCartaDeInstruccion(dtoDocumento, server);
                        break;

                    case "WEBRECORDATORIO":
                        documentuUtil.GenerarRecordatoriodePago(dtoDocumento, server);
                        break;

                    case "WEBLIQUIDACION":
                        documentuUtil.GenerarLiquidacionConsumo(dtoDocumento, server);
                        break;

                    case "MICWEBPROFORMA":
                    case "MICWEBPROFORMAINCM":
                        documentuUtil.GenerarProformaUnificada(dtoDocumento, server);
                        break;

                    case "MICWEBCARTA":
                        string nombreCampania = _campaniaInfraestructure.GetNombreCampaniaByCodigoCampania(dtoDocumento.CodigoCampania);
                        documentuUtil.GenerarCartaMicroWeb(dtoDocumento, server, nombreCampania);
                        break;
                    #endregion Fin MicroEmpresa

                    #region Olla de Oro
                    case "OROWEBACTACANC":
                        documentuUtil.GeneraActaCancelacion(dtoDocumento, server);
                        break;

                    case "OROWEBINSTRUCCIO":
                        documentuUtil.GenerarCartaDeInstruccion(dtoDocumento, server);
                        break;

                    case "OROWEBCUSTODIA":
                    case "OROWEBPRENDA":
                        documentuUtil.GeneraContratoPrendaCustodia(dtoDocumento, server);
                        break;

                    case "OROWEBLIQUIDACIO":
                        documentuUtil.GenerarLiquidacionConsumoOro(dtoDocumento, server);
                        break;

                    case "OROWEBPROFORMA":
                        documentuUtil.GenerarProformaUnificadaOro(dtoDocumento, server);
                        break;

                    case "OROWEBRECORDATOR":
                        documentuUtil.GenerarRecordatoriodePagoOro(dtoDocumento, server);
                        break;

                    case "OROWEBSOLVENTA":
                    case "OROWEBSOLSALARIO":
                        documentuUtil.GenerarSolicitudOro(dtoDocumento, server);
                        break;

                    #endregion Fin Olla de Oro

                    #region Casas Comerciales
                    case "CCOWEBPAGARE":
                    case "CCOWEBGASCBR":
                        documentuUtil.GenerarLogoCasaComercial(dtoDocumento, server);
                        break;

                    case "CCOWEBTABAMR":
                        documentuUtil.GenerarLogoCasaComercial(dtoDocumento, server);
                        documentuUtil.GenerarTablaAmortizacionCasasComerciales(dtoDocumento, server);
                        break;

                    case "CCOWEBSOLDEP":
                    case "CCOWEBSOLIND":
                        documentuUtil.GenerarLogoCasaComercial(dtoDocumento, server);
                        documentuUtil.GenerarTablaReferenciasPersonalesCCOWEB(dtoDocumento, server);
                        break;
                    #endregion Fin Casas Comerciales

                    #region Credito no Cliente
                    case "WEBNCCARTAINSTRUCCION":
                        documentuUtil.GenerarCartaDeInstruccionNoCliente(dtoDocumento, server);
                        break;
                    case "WEBNCPROFORMACRYA":
                        documentuUtil.GenerarProformaUnificadaRapiditoNoCliente(dtoDocumento, server);
                        break;
                    case "WEBNCRECPAG":
                        documentuUtil.GenerarRecordatoriodePagoRapiditoNoCliente(dtoDocumento, server);
                        break;
                        #endregion
                }

                if (contador == 0)
                {
                    serverTotal.LoadDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
                    serverTotal.Document.DefaultParagraphProperties.Alignment = ParagraphAlignment.Justify;

                }
                else
                {
                    string PageBreak = new('\u000C', 1);
                    serverTotal.Document.AppendText(PageBreak);
                    serverTotal.Document.AppendDocumentContent(server.Document.Range, InsertOptions.KeepSourceFormatting);
                }

                server.LoadDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
                using (FileStream pdfFileStream = new(dtoDocumento.PathPdf, FileMode.Create))
                {
                    DevExpress.XtraPrinting.PdfExportOptions pdfOptions = new()
                    {
                        ConvertImagesToJpeg = true,
                        ImageQuality = DevExpress.XtraPrinting.PdfJpegImageQuality.Medium
                    };
                    server.ExportToPdf(pdfFileStream, pdfOptions);
                }

                File.Delete(dtoDocumento.PathDoc);
                File.Delete(dtoDocumento.PathRtf);
                server.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private bool HasToBeGenerated(DtoDocumento dtoDocumento, DtoInformacionDocumentos dtoInformacionDocumentos)
        {
            if ((dtoDocumento.Codigo == "WEBNCCONTRATO" || dtoDocumento.Codigo == "WEBCONTRATO") && dtoDocumento.SeCreoCuenta == false)
            {
                Documento documento = _documentoRepository.GetDocumentoByCodigo(dtoDocumento.Codigo).Result;
                dtoInformacionDocumentos.RutasPictore = dtoInformacionDocumentos.RutasPictore.FindAll(i => !i.RutaLocal!.Contains(documento.NombreDocumento));
                return false;
            }

            if (dtoDocumento.Codigo == "WEBNCSEGFAMSEGCON" && dtoDocumento.ValorSeguro == 0)
            {
                return false;
            }

            if (dtoDocumento.Codigo == "OROWEBSOLVENTA" || dtoDocumento.Codigo == "OROWEBSOLSALARIO")
            {
                string actividadEconomica = _clienteInfraestructure.GetActividadEconomicaCliente(dtoDocumento.Identificacion!);

                if (!string.IsNullOrEmpty(actividadEconomica))
                {
                    if ((dtoDocumento.Codigo == "OROWEBSOLVENTA" && actividadEconomica == "SAL") || dtoDocumento.Codigo == "OROWEBSOLVENTA" && actividadEconomica == "REN")
                    {
                        return false;
                    }
                    if (dtoDocumento.Codigo == "OROWEBSOLSALARIO" && actividadEconomica == "VEN")
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<IEnumerable<DtoResponse>> CopyTotalDocumentInIndividualDocumentsByIdTramite(DtoRequest dtoRequest)
        {
            List<DtoResponse> result = new();
            DtoTramite dtoTramite = await _tramiteInfraestructure.GetDtoTramiteByIdTramite(dtoRequest.IdTramite);
            Parametro parametro = await _configuracionInfraestructure.GetParametroByNombre("RUTADOCDIGITAL");
            if (dtoTramite.IdTramite != 0)
            {
                await InsertLogsByTramite(dtoTramite);

                if (dtoRequest.GenerarDocumentosFaltantes)
                {
                    List<LogGeneracionDocumento> logs = await _logGeneracionDocumentoInfraestructure.GetLogsGeneracionByEstado(false);
                    foreach (LogGeneracionDocumento log in logs)
                    {
                        dtoTramite = await _tramiteInfraestructure.GetDtoTramiteByIdTramite(dtoRequest.IdTramite);
                        await CopyTotalDocumentInIndividualDocumentsByIdTramite(result, log, dtoTramite, parametro.ValorCadena!);
                    }
                }
                else
                {
                    LogGeneracionDocumento log = await _logGeneracionDocumentoInfraestructure.GetLogGeneracionByIdTramite(dtoTramite.IdTramite);
                    await CopyTotalDocumentInIndividualDocumentsByIdTramite(result, log, dtoTramite, parametro.ValorCadena!);
                }
                return result;
            }
            else
            {
                throw new Exception(ResponseConstant.TramitesNotFound);
            }
        }

        private async Task CopyTotalDocumentInIndividualDocumentsByIdTramite(List<DtoResponse> result, LogGeneracionDocumento log, DtoTramite dtoTramite, string rutaDocDigital)
        {
            if (!log.Estado)
            {
                List<DtoRutasPictore> rutasPictore = _pictoreInfraestructure.GetRutasPictore(log.Identificacion!, dtoTramite.LotePictor, dtoTramite.IdTramite);
                DtoGeneration dtoGeneration = await CopyTotalDoumentInIndividualDocuments(dtoTramite, rutaDocDigital, rutasPictore);

                log.FechaProcesoFin = DateTime.Now;
                log.Estado = dtoGeneration.Estado;
                log.Error = JsonConvert.SerializeObject(dtoGeneration.Error);
                await _logGeneracionDocumentoInfraestructure.UpdateLog(log);

                if (!dtoGeneration.Estado)
                {
                    result.Add(new()
                    {
                        IdTramite = dtoGeneration.IdTramite,
                        ErrorGeneracion = dtoGeneration.Error.ErrorGeneracionFront,
                        ErrorRutaCentral = dtoGeneration.Error.ErrorRutaCentralFront
                    });
                }
            }
        }

        private async Task<DtoGeneration> CopyTotalDoumentInIndividualDocuments(DtoTramite dtoTramite, string rutaDocDigital, List<DtoRutasPictore> rutasPictore)
        {
            List<DtoErrorGeneracionInterno> dtoErrorGeneracionInternos = new();
            List<DtoErrorRutaCentralInterno> dtoErrorRutaCentralInternos = new();
            string rutaLocal = rutaDocDigital + dtoTramite.LotePictor;
            List<DtoDocumentoFlujoWeb> dtoDocumentosFlujoWeb = await GetDocumentosToGenerateByFlujoWeb(dtoTramite.IdFlujoWeb, dtoTramite.FechaDesembolso);
            try
            {
                if (!Directory.Exists(rutaLocal))
                {
                    Directory.CreateDirectory(rutaLocal);
                }
                DtoDocumentoFlujoWeb dtoTotalDocument = dtoDocumentosFlujoWeb.Find(doc => doc.ObjetoFormato?.Length == 0) ?? new DtoDocumentoFlujoWeb();

                if (dtoTotalDocument.IdDocumento != 0)
                {
                    string totalDocumentNamePath = rutaLocal + "\\" + dtoTotalDocument.NombreDocumento;

                    if (!File.Exists(totalDocumentNamePath))
                    {
                        DtoRequest dtoRequest = new()
                        {
                            IdFirma = _reconocimientoFacialInfraestructure.GetIdFirmaByNumeroTramite(dtoTramite.NumeroTramite!)
                        };
                        await _anfRepository.RecuperarDocumentosFirmados(dtoRequest);
                        totalDocumentNamePath = Path.Combine(_configuration.GetSection(ComunicationConstant.RutaDriveDocumentos).Value!, _configuration.GetSection(ComunicationConstant.RutaDocumentos).Value!) + "\\" + dtoRequest.IdFirma + ".pdf";
                    }

                    dtoErrorGeneracionInternos = ReplaceTotalDocumentoInLocalRoute(dtoDocumentosFlujoWeb, rutaLocal, totalDocumentNamePath);
                    dtoErrorRutaCentralInternos = CopyLocalFilesToCentralPictor(rutasPictore);

                }
                else
                {
                    dtoErrorGeneracionInternos.Add(new()
                    {
                        Codigo = "Tramite con Flujo: " + dtoTramite.IdFlujoWeb,
                        Estado = false,
                        Error = "No Existe Documento Total Generado en parametrización en la fecha de desembolso: "+ dtoTramite.FechaDesembolso
                    });
                }
            }
            catch (Exception ex)
            {
                dtoErrorGeneracionInternos.Add(new()
                {
                    Codigo = "Method CopyTotalDoumentInIndividualDocuments",
                    Estado = false,
                    Error = ex.Message
                });
            }

            return ReturnDtoGenerationWithValidationIfHasError(dtoTramite.IdTramite, dtoErrorGeneracionInternos, dtoErrorRutaCentralInternos);
        }

        public async Task<IEnumerable<DtoResponse>> CopyTotalDocumentInIndividualDocumentsByDates(DtoRequest dtoRequest)
        {
            List<DtoResponse> result = new();
            List<DtoTramite> dtoTramites = await _tramiteInfraestructure.GetTramitesByDateRange(dtoRequest.FechaInicio, dtoRequest.FechaFin);
            Parametro parametro = await _configuracionInfraestructure.GetParametroByNombre("RUTADOCDIGITAL");
            if (dtoTramites.Count != 0)
            {
                await InsertLogsByTramites(dtoTramites);

                List<LogGeneracionDocumento> logs = dtoRequest.GenerarDocumentosFaltantes ? await _logGeneracionDocumentoInfraestructure.GetLogsGeneracionByEstado(false) : await GetLogsGenerationDocumentWithStatusZeroByTramites(dtoTramites);

                logs.ForEach(async log =>
                {
                    DtoTramite dtoTramite = await _tramiteInfraestructure.GetDtoTramiteByIdTramite(dtoRequest.IdTramite);
                    await CopyTotalDocumentInIndividualDocumentsByIdTramite(result, log, dtoTramite, parametro.ValorCadena!);
                });
                return result;
            }
            else
            {
                throw new Exception(ResponseConstant.TramitesNotFound);
            }
        }

        private async Task InsertLogsByTramite(DtoTramite tramite)
        {
            LogGeneracionDocumento logGeneracionDocumento = MapperFromTramiteAndIdentificacion(tramite);
            logGeneracionDocumento.FechaProcesoInicio = DateTime.Now;
            await _logGeneracionDocumentoInfraestructure.CreateLog(logGeneracionDocumento);
        }

        private static List<DtoErrorGeneracionInterno> ReplaceTotalDocumentoInLocalRoute(List<DtoDocumentoFlujoWeb> dtoDocumentosFlujoWeb, string rutaDocDigital, string totalDocumentNamePath)
        {
            List<DtoErrorGeneracionInterno> dtoErrorGeneracionInternos = new();
            try
            {
                foreach (DtoDocumentoFlujoWeb item in dtoDocumentosFlujoWeb)
                {
                    try
                    {
                        string localRoute = Path.Combine(rutaDocDigital, item.NombreDocumento) + ".pdf";
                        if (!File.Exists(localRoute))
                        {
                            File.Copy(totalDocumentNamePath, localRoute);
                        }
                    }
                    catch (Exception ex)
                    {
                        dtoErrorGeneracionInternos.Add(new()
                        {
                            Codigo = item.Codigo,
                            Estado = false,
                            Error = ex.Message
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                dtoErrorGeneracionInternos.Add(new()
                {
                    Codigo = "Method ReplaceTotalDocumentoInLocalRoute",
                    Estado = false,
                    Error = ex.Message
                });
            }
            return dtoErrorGeneracionInternos;
        }

    }
}
