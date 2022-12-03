using MunicipalityTax.API.Data.Entities;

namespace MunicipalityTax.API.Data.Interfaces
{
    public interface IMunicipalityRepository
    {
        Task<Municipality> GetMunicipalityAsync(string municipalityname);
    }
}
