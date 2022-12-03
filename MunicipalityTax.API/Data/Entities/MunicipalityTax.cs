using System.ComponentModel.DataAnnotations;

namespace MunicipalityTax.API.Data.Entities
{
    public class MunicipalityTax
    {
        [Key]
        public int MunicipalityTaxId { get; set; }
        public int MunicipalityId { get; set; }
        public Municipality Municipality { get; set; }
        public int TaxTypeId { get; set; }
        public TaxType TaxType { get; set; }
        public decimal TaxAmount { get; set; }
        public DateTime StartDtm { get; set; }
        public DateTime EndDtm { get; set; }
    }
}
