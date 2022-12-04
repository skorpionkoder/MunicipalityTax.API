using System.ComponentModel.DataAnnotations;
using MunicipalityTax.API.Data.Entities;

namespace MunicipalityTax.API.Models
{
    public class MunicipalityTaxModel
    {
        [Required]
        public string MunicipalityName { get; set; }
        [Required]
        public string TaxType { get; set; }
        [Required]
        public decimal TaxAmount { get; set; }
        [Required]
        public DateTime StartDtm { get; set; }
    }
}
