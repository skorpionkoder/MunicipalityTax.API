using Microsoft.AspNetCore.Mvc;
using MunicipalityTax.API.Data.Entities;
using MunicipalityTax.API.Data.Interfaces;

namespace MunicipalityTax.API.Controllers
{
    [ApiController]
    [Route("api/municipalitytax")]
    public class MunicipalityTaxController : ControllerBase
    {
        private readonly IMunicipalityTaxRepository _municipalityTaxRepository;
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly ILogger<MunicipalityTaxController> _logger;

        public MunicipalityTaxController(IMunicipalityTaxRepository municipalityTaxRepository, IMunicipalityRepository municipalityRepository, ILogger<MunicipalityTaxController> logger)
        {
            _municipalityTaxRepository = municipalityTaxRepository ?? throw new ArgumentNullException(nameof(municipalityTaxRepository));
            _municipalityRepository = municipalityRepository ?? throw new ArgumentNullException(nameof(municipalityRepository));
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetMunicipalityTax(string municipalityName, DateTime taxDate)
        {
            try
            {
                _logger.LogInformation($"HttpGet: GetMunicipalityTax(), Request: MunicipalityName {municipalityName}; Date {taxDate}");

                var municipality = await _municipalityRepository.GetMunicipalityAsync(municipalityName);

                if (municipality == null) return NotFound();

                // Get Municipality Tax
                var municipalityTaxes = await _municipalityTaxRepository.GetMunicipalityTaxesAsync(municipality.MunicipalityId, taxDate);
                
                var municipalityTax = municipalityTaxes.OrderByDescending(t => t.TaxTypeId)
                    .Select(t => t.TaxAmount)
                    .FirstOrDefault();

                _logger.LogInformation($"HttpGet: GetMunicipalityTax(), Response: MunicipalityTax {municipalityTax}");

                return Ok(municipalityTax);
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception occurred while getting Municipality Tax.", ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
