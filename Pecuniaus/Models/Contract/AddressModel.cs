using System.ComponentModel.DataAnnotations;
using Pecuniaus.Utilities.Validation;

namespace Pecuniaus.Models.Contract
{
    public class AddressModel
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [Display(Name = "City", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string City { get; set; }

        public string Country { get; set; }

        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "EmailValidReq", ErrorMessage = null)]
        [Display(Name = "Email", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string Email { get; set; }

        [Display(Name = "TelephoneNumber", ResourceType = typeof(Resources.Contract.DataEntry)), RegularExpression(ValidationRules.Telephone, ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "TelephoneVal")]
        [DataType(DataType.PhoneNumber)]
        public string Phone1 { get; set; }

        [Display(Name = "OtherTelephoneNumber", ResourceType = typeof(Resources.Contract.DataEntry)), RegularExpression(ValidationRules.Telephone, ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "OtherTelephoneVal")]
        [DataType(DataType.PhoneNumber)]
        public string Phone2 { get; set; }

        public string State { get; set; }

        //[Required]
        //[Display(Name = "States", ResourceType = typeof(Resources.Contract.DataEntry))]
        //public int StateID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "ProvincesReq")]
        [Display(Name = "Provinces", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int StateId { get; set; }
    }
}
