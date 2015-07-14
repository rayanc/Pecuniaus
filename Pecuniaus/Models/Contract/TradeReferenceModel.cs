using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Pecuniaus.Utilities.Validation;


namespace Pecuniaus.Models.Contract
{
    public class TradeReferenceModel:BaseModel
    {
        public int Id { get; set; }
        public long ReferenceId { get; set; }
        [Required]
        [Display(Name = "Name of the Reference")]
        public string ReferenceName { get; set; }
        [Required]
        [Display(Name = "Telephone Number"), RegularExpression(ValidationRules.Telephone, ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "TelephoneVal")]

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public long MerchantId { get; set; }
        public long ContractId { get; set; }
    }
}
