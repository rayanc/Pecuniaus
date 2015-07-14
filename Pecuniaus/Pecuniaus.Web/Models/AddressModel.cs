using System.ComponentModel.DataAnnotations;
using Pecuniaus.Utilities.Validation;

namespace Pecuniaus.Web.Models
{
    public class AddressModel
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
       
        [Display(Name = "City", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string city { get; set; }

        public string Country { get; set; }

        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string email { get; set; }

        [Display(Name = "TelephoneNumber", ResourceType = typeof(Resources.Contract.DataEntry))]
        [RegularExpression(ValidationRules.Telephone, ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "TelephoneVal")]
        [DataType(DataType.PhoneNumber)]
        public string phone1 { get; set; }
     
        [Display(Name = "OtherTelephoneNumber", ResourceType = typeof(Resources.Contract.DataEntry))]
        [RegularExpression(ValidationRules.Telephone, ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "OtherTelephoneVal")]
        [DataType(DataType.PhoneNumber)]
        public string phone2 { get; set; }
        
        public string State { get; set; }

        //[Required]
        //[Display(Name = "States", ResourceType = typeof(Resources.Contract.DataEntry))]
        //public int StateID { get; set; }

        [Required]
        [Display(Name = "Provinces", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int? stateId { get; set; }

    }
}