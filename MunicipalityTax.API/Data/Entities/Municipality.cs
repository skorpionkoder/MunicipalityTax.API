using System.ComponentModel.DataAnnotations;

namespace MunicipalityTax.API.Data.Entities
{
    public class Municipality
    {
        [Key]
        public int MunicipalityId { get; set; }
        public string Name { get; set; }
    }
}
