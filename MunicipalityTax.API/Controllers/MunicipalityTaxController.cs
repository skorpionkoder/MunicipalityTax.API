using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MunicipalityTax.API.Data;
using MunicipalityTax.API.Data.Entities;
using MunicipalityTax.API.Data.Interfaces;
using MunicipalityTax.API.Models;

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

        [HttpPost]
        public async Task<ActionResult<MunicipalityTaxModel>> Post(MunicipalityTaxModel model)
        {
            try
            {
                _logger.LogInformation($"HttpPost Request: MunicipalityTax {model}");

                var municipality = _municipalityRepository.GetMunicipalityAsync(model.MunicipalityName);

                var municipalityTax = new Data.Entities.MunicipalityTax();
                if (municipality.Result == null)
                {
                    var municipalityEntity = new Municipality()
                    {
                        Name = model.MunicipalityName
                    };
                    _municipalityRepository.Add(municipalityEntity);
                    await _municipalityRepository.SaveChangesAsync();
                    municipalityTax.MunicipalityId = municipalityEntity.MunicipalityId;
                }
                else municipalityTax.MunicipalityId = municipality.Result.MunicipalityId;

                municipalityTax.TaxTypeId = (int)Enum.Parse<TaxSchedule>(model.TaxType);
                municipalityTax.TaxAmount = model.TaxAmount;
                municipalityTax.StartDtm = model.StartDtm;
                
                CreateTaxSchedule(model, municipalityTax);

                // Create Municipality Tax
                _municipalityTaxRepository.Add(municipalityTax);
                if (await _municipalityTaxRepository.SaveChangesAsync())
                {
                    _logger.LogInformation($"HttpPost Response: Municipality Tax {municipalityTax.MunicipalityTaxId}");
                    return Created($"api/municipalitytax/{municipalityTax.MunicipalityTaxId}", model);
                }
            }
            catch (ApplicationException ex)
            {
                _logger.LogError("An exception occurred while creating Municipality Tax.", ex.Message);
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception occurred while creating Municipality Tax.", ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MunicipalityTaxModel>> PutMunicipalityTax(int id, MunicipalityTaxModel model)
        {
            try
            {
                _logger.LogInformation($"HttpPut Request: MunicipalityTax {model}");

                var municipalityTax = _municipalityTaxRepository.GetMunicipalityTaxAsync(id);

                municipalityTax.Result.TaxTypeId = (int)Enum.Parse<TaxSchedule>(model.TaxType);
                municipalityTax.Result.TaxAmount = model.TaxAmount;
                municipalityTax.Result.StartDtm = model.StartDtm;

                CreateTaxSchedule(model, municipalityTax.Result);

                // Update Municipality Tax
                if (await _municipalityTaxRepository.SaveChangesAsync())
                {
                    _logger.LogInformation($"HttpPut Response: Municipality Tax {municipalityTax.Result.MunicipalityId}");
                    return Created($"api/municipalitytax/{municipalityTax.Result.MunicipalityTaxId}", model);
                }
            }
            catch (ApplicationException ex)
            {
                _logger.LogError("An exception occurred while updating Municipality Tax.", ex.Message);
                return this.StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception occurred while updating Municipality Tax.", ex.Message);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        private static void CreateTaxSchedule(MunicipalityTaxModel model, Data.Entities.MunicipalityTax municipalityTax)
        {
            switch (municipalityTax.TaxTypeId)
            {
                case 1:
                    municipalityTax.EndDtm = model.StartDtm.AddYears(1);
                    break;
                case 2:
                    municipalityTax.EndDtm = model.StartDtm.AddMonths(1);
                    break;
                case 3:
                    municipalityTax.EndDtm = model.StartDtm.AddDays(7);
                    break;
                case 4:
                    municipalityTax.EndDtm = model.StartDtm.AddDays(1);
                    break;
                default:
                    throw new InvalidDataException($"Invalid Tax Type {model.TaxType}");
            }
        }
    }
}
