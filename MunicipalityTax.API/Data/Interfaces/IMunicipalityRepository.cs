using MunicipalityTax.API.Data.Entities;

namespace MunicipalityTax.API.Data.Interfaces
{
    public interface IMunicipalityRepository
    {
        void Add<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<Municipality> GetMunicipalityAsync(string municipalityname);
    }
}
