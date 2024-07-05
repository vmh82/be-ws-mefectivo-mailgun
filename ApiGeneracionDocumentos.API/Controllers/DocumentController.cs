using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Domain.Services;
using ApiGeneracionDocumentos.Entity;
using ApiGeneracionDocumentos.Entity.Constants;
using ApiGeneracionDocumentos.Entity.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ApiGeneracionDocumentos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : Controller
    {
        private readonly ITramiteInfraestructure _tramiteInfraestructure;
        private readonly IDocumentoInfraestructure _documentoInfraestructure;
        private readonly ILogGeneracionDocumentoInfraestructure _logGeneracionDocumentoInfraestructure;
        private readonly IAnfInfraestructure _anfInfraestructure;
        private readonly IDocumentoInfraestructure _documentationDocumentoInfraestructure;
        public DocumentController(ITramiteInfraestructure tramiteInfraestructure, IDocumentoInfraestructure documentoInfraestructure, ILogGeneracionDocumentoInfraestructure logGeneracionDocumentoInfraestructure, IDocumentoInfraestructure documentationDocumentoInfraestructure, IAnfInfraestructure anfInfraestructure)
        {
            _tramiteInfraestructure = tramiteInfraestructure;
            _documentoInfraestructure = documentoInfraestructure;
            _logGeneracionDocumentoInfraestructure = logGeneracionDocumentoInfraestructure;
            _documentationDocumentoInfraestructure = documentationDocumentoInfraestructure;
            _anfInfraestructure = anfInfraestructure;
        }
        [HttpPost]
        [Route("DownloadSignedDocument")]
        public async Task<IActionResult> DownloadSignedDocument([FromBody] DtoRequest dtoRequest)
        {
            try
            {
                await _anfInfraestructure.RecuperarDocumentosFirmados(dtoRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseConstant.ErrorDocumentGeneration + ex.Message);
            }
        }

        [HttpPost]
        [Route("ResetLogs")]
        public async Task<IActionResult> ResetLogs()
        {
            try
            {
                await _logGeneracionDocumentoInfraestructure.ResetLogs();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseConstant.ErrorDocumentGeneration + ex.Message);
            }
        }

        [HttpPost]
        [Route("DeleteLogs")]
        public async Task<IActionResult> DeleteLogs()
        {
            try
            {
                await _logGeneracionDocumentoInfraestructure.DeleteLogs();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseConstant.ErrorDocumentGeneration + ex.Message);
            }
        }

        [HttpPost]
        [Route("DocumentGenerationByClient")]
        public async Task<IActionResult> DocumentGenerationByClient([FromBody] DtoRequest dtoRequest)
        {
            try
            {
                List<DtoTramite> tramites = new()
                {
                    await _tramiteInfraestructure.GetTramiteByIdentificacionAndFlujoWebAndFechaDesem(dtoRequest.Identificacion, dtoRequest.CodigoFlujoWeb, dtoRequest.FechaDesembolso)
                };
                if (tramites.Count != 0)
                {
                    IEnumerable<DtoResponse> result = await _documentoInfraestructure.DocumentGenerationByTramites(tramites, dtoRequest.GenerarDocumentosFaltantes);
                    return Ok(result);
                }
                else
                {
                    return NotFound(ResponseConstant.TramitesNotFound);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseConstant.ErrorDocumentGeneration + ex.ToString());
            }
        }

        [HttpPost]
        [Route("DocumentGenerationByIdTramite")]
        public async Task<IActionResult> DocumentGenerationByIdTramite([FromBody] DtoRequest dtoRequest)
        {
            try
            {
                DtoTramite tramite = await _tramiteInfraestructure.GetDtoTramiteByIdTramite(dtoRequest.IdTramite);
                List<DtoTramite> tramites = new()
                {
                    tramite
                };
                if (tramites.Count != 0)
                {
                    IEnumerable<DtoResponse> result = await _documentoInfraestructure.DocumentGenerationByTramites(tramites, dtoRequest.GenerarDocumentosFaltantes);
                    return Ok(result);
                }
                else
                {
                    return NotFound(ResponseConstant.TramitesNotFound);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseConstant.ErrorDocumentGeneration + ex.Message);
            }
        }

        [HttpPost]
        [Route("DocumentGenerationByDates")]
        public async Task<IActionResult> DocumentGenerationByDates([FromBody] DtoRequest dtoRequest)
        {
            try
            {
                List<DtoTramite> tramites = await _tramiteInfraestructure.GetTramitesByDateRange(dtoRequest.FechaInicio, dtoRequest.FechaFin);
                if (tramites.Count != 0)
                {
                    IEnumerable<DtoResponse> result = await _documentoInfraestructure.DocumentGenerationByTramites(tramites, dtoRequest.GenerarDocumentosFaltantes);
                    return Ok(result);
                }
                else
                {
                    return NotFound(ResponseConstant.TramitesNotFound);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseConstant.ErrorDocumentGeneration + ex.ToString());
            }
        }

        [HttpPost]
        [Route("CopyTotalDocumentInIndividualDocumentsByIdTramite")]
        public async Task<IActionResult> CopyTotalDocumentInIndividualDocumentsByIdTramite([FromBody] DtoRequest dtoRequest)
        {
            try
            {
                IEnumerable<DtoResponse> result = await _documentoInfraestructure.CopyTotalDocumentInIndividualDocumentsByIdTramite(dtoRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseConstant.ErrorDocumentCopy + ex.ToString());
            }
        }

        [HttpPost]
        [Route("CopyTotalDocumentInIndividualDocumentsByDates")]
        public async Task<IActionResult> CopyTotalDocumentInIndividualDocumentsByDates([FromBody] DtoRequest dtoRequest)
        {
            try
            {
                IEnumerable<DtoResponse> result = await _documentoInfraestructure.CopyTotalDocumentInIndividualDocumentsByDates(dtoRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseConstant.ErrorDocumentCopy + ex.ToString());
            }
        }
    }
}
