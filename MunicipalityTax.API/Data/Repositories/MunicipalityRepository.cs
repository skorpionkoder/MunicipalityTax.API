using Microsoft.EntityFrameworkCore;
using MunicipalityTax.API.Data.Contexts;
using MunicipalityTax.API.Data.Entities;
using MunicipalityTax.API.Data.Interfaces;

namespace MunicipalityTax.API.Data.Repositories
{
    public class MunicipalityRepository : IMunicipalityRepository
    {
        private readonly MunicipalityContext _context;
        private readonly ILogger<MunicipalityRepository> _logger;

        public MunicipalityRepository(MunicipalityContext context, ILogger<MunicipalityRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public async Task<Municipality> GetMunicipalityAsync(string municipalityname)
        {
            _logger.LogInformation($"Getting Municipality ID for {municipalityname}.");

            var municipality = _context.Municipality
                .Where(m => m.Name == municipalityname);

            return await municipality.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempting to save the changes in the context");
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
