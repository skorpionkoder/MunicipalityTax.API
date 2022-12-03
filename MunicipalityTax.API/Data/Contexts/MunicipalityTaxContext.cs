using Microsoft.EntityFrameworkCore;

namespace MunicipalityTax.API.Data.Contexts
{
    public class MunicipalityTaxContext : DbContext
    {
        public MunicipalityTaxContext(DbContextOptions<MunicipalityTaxContext> options) : base(options)
        {
        }

        public DbSet<Entities.MunicipalityTax> MunicipalityTax { get; set; }
    }
}
