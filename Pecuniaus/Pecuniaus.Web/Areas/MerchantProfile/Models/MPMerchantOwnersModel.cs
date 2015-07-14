using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    [FluentValidation.Attributes.Validator(typeof(Pecuniaus.Validators.MPMerchantOwnersValidator))]
    public class MPMerchantOwnersModel
    {
       
        public Int64 OwnerId { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Owners))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Owners))]
        public string LastName { get; set; }

        [Display(Name = "OwnerIdentification", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Owners))]
        public string OwnerIdentification { get; set; }

        [Display(Name = "DateofBirth", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Owners))]
        [DataType(DataType.Date)]
        public DateTime? DateofBirth { get; set; }

        [Display(Name = "DateBecameOwner", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Owners))]
        [DataType(DataType.Date)]
        public DateTime? DateBecameOwner { get; set; }

        [Display(Name = "IsAuthorized", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Owners))]
        public bool IsAuthorized { get; set; }
    }
}