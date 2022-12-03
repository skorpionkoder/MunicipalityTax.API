using Microsoft.EntityFrameworkCore;
using MunicipalityTax.API.Data.Contexts;
using MunicipalityTax.API.Data.Entities;
using MunicipalityTax.API.Data.Interfaces;

namespace MunicipalityTax.API.Data.Repositories
{
    public class TaxTypeRepository : ITaxTypeRepository
    {
        private readonly TaxTypeContext _context;
        private readonly ILogger<TaxTypeRepository> _logger;

        public TaxTypeRepository(TaxTypeContext context, ILogger<TaxTypeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TaxType> GetTaxTypeAsync(string taxTypeName)
        {
            _logger.LogInformation($"Getting Tax Type ID for {taxTypeName} type.");

            var taxType = _context.TaxType
                .Where(t => t.TypeName == taxTypeName);

            return await taxType.FirstOrDefaultAsync();
        }
    }
}
