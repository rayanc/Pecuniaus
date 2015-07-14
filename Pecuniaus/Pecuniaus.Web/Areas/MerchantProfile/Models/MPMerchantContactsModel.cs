using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    [FluentValidation.Attributes.Validator(typeof(Pecuniaus.Validators.MPMerchantContactsValidator))]
    public class MPMerchantContactsModel
    {
        public Int64 ContactId { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contacts))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contacts))]
        public string LastName { get; set; }

        [Display(Name = "Position", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contacts))]
        public string Position { get; set; }

        [Display(Name = "DateofBirth", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contacts))]
        [DataType(DataType.Date)]
        public DateTime? DateofBirth { get; set; }

        [Display(Name = "IsOwner", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contacts))]
        public string IsOwner { get; set; }

        [Display(Name = "OwnerIdentification", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contacts))]
        public string OwnerIdentification { get; set; }
    }
}