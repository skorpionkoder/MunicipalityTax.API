using MunicipalityTax.API.Data.Entities;

namespace MunicipalityTax.API.Models
{
    public class MunicipalityTaxModel
    {
        public string MunicipalityName { get; set; }
        public string TaxType { get; set; }
        public decimal TaxAmount { get; set; }
        public DateTime StartDtm { get; set; }
    }
}
