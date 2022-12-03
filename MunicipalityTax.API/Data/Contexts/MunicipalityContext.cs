using Microsoft.EntityFrameworkCore;
using MunicipalityTax.API.Data.Entities;

namespace MunicipalityTax.API.Data.Contexts
{
    public class MunicipalityContext : DbContext
    {
        public MunicipalityContext(DbContextOptions<MunicipalityContext> options) : base(options)
        {
        }

        public DbSet<Municipality> Municipality { get; set; }
    }
}
