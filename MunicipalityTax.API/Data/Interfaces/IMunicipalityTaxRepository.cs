namespace MunicipalityTax.API.Data.Interfaces
{
    public interface IMunicipalityTaxRepository
    {
        // General
        void Add<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        // Municipality Taxes
        Task<ICollection<Entities.MunicipalityTax>> GetMunicipalityTaxesAsync(int municipalityId, DateTime taxDate);
    }
}
