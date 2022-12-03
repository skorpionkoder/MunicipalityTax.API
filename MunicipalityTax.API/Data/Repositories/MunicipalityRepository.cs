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

        public async Task<Municipality> GetMunicipalityAsync(string municipalityname)
        {
            _logger.LogInformation($"Getting Municipality ID for {municipalityname}.");

            var municipality = _context.Municipality
                .Where(m => m.Name == municipalityname);

            return await municipality.FirstOrDefaultAsync();
        }
    }
}
