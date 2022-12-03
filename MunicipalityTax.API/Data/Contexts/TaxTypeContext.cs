using Microsoft.EntityFrameworkCore;
using MunicipalityTax.API.Data.Entities;

namespace MunicipalityTax.API.Data.Contexts
{
    public class TaxTypeContext : DbContext
    {
        public TaxTypeContext(DbContextOptions<TaxTypeContext> options) : base(options)
        {
        }

        public DbSet<TaxType> TaxType { get; set; }
    }
}
