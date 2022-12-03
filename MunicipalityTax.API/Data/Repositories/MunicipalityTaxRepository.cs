using Microsoft.EntityFrameworkCore;
using MunicipalityTax.API.Data.Contexts;
using MunicipalityTax.API.Data.Interfaces;

namespace MunicipalityTax.API.Data.Repositories
{
    public class MunicipalityTaxRepository : IMunicipalityTaxRepository
    {
        private readonly MunicipalityTaxContext _context;
        private readonly ILogger<MunicipalityTaxRepository> _logger;

        public MunicipalityTaxRepository(MunicipalityTaxContext context, ILogger<MunicipalityTaxRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public async Task<ICollection<Entities.MunicipalityTax>> GetMunicipalityTaxesAsync(int municipalityId, DateTime taxDate)
        {
            _logger.LogInformation($"Getting Municipality Taxes for ID {municipalityId} and Date {taxDate}.");

            var municipalityTax = _context.MunicipalityTax
                .Where(m => m.MunicipalityId == municipalityId && taxDate >= m.StartDtm && taxDate <= m.EndDtm);

            return await municipalityTax.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempting to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
