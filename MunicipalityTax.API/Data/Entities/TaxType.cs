using System.ComponentModel.DataAnnotations;

namespace MunicipalityTax.API.Data.Entities
{
    public class TaxType
    {
        [Key]
        public int TaxTypeId { get; set; }
        public string TypeName { get; set; }
    }
}
