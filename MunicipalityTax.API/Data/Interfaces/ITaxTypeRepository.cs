using MunicipalityTax.API.Data.Entities;

namespace MunicipalityTax.API.Data.Interfaces
{
    public interface ITaxTypeRepository
    {
        Task<TaxType> GetTaxTypeAsync(string taxTypeName);
    }
}
