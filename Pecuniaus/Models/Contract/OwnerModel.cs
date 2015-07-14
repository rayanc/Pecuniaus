using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Pecuniaus.Utilities.Validation;

namespace Pecuniaus.Models.Contract
{
    public class OwnerModel : BaseModel
    {
        public OwnerModel()
        {
            States = new List<SelectListItem>();
        }

        public int Id { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }

        [Display(Name = "OwnerFirstName", ResourceType = typeof(Resources.Contract.DataEntry))]
        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "OwnerFirstNameRequired")]
        public string OwnerFirstName { get; set; }

        [Display(Name = "OwnerLastName", ResourceType = typeof(Resources.Contract.DataEntry))]
        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "OwnerLastNameRequired")]
        public string OwnerLastName { get; set; }

        public Int64 OwnerId { get; set; }
        public Int64 MerchanId { get; set; }

        public Int64 contactId { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "PhoneNumberReq")]
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.Contract.DataEntry)), RegularExpression(ValidationRules.Telephone, ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "PhoneNumberVal")]
        [DataType(DataType.PhoneNumber)]
        public string Phone1 { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "CellNumberReq")]
        [Display(Name = "CellNumber", ResourceType = typeof(Resources.Contract.DataEntry)), RegularExpression(ValidationRules.Telephone, ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "CellNumberVal")]
        [DataType(DataType.PhoneNumber)]
        public string CellNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "AddressLine1Req")]
        [Display(Name = "AddressLine1", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string AddressLine1 { get; set; }

        [Display(Name = "AddressLine2", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string AddressLine2 { get; set; }

        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "OwnerValidEmail", ErrorMessage = null)]
        [Display(Name = "Email", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string Email { get; set; }

        public string Ssn { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "OwnerDOBReq")]
        [Display(Name = "OwnerDOB", ResourceType = typeof(Resources.Contract.DataEntry))]
        [DataType(DataType.Date)]
        public DateTime OwnerDOB { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "PassportNumberReq")]
        [Display(Name = "PassportNumber", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string PassportNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "AuthorizedReq")]
        [Display(Name = "Authorized", ResourceType = typeof(Resources.Contract.DataEntry))]
        public bool Authorized { get; set; }

        public string ZipId { get; set; }
        public string CityId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "StatesReq")]
        [Display(Name = "States", ResourceType = typeof(Resources.Contract.DataEntry))]
        public long? StateID { get; set; }
        public string State { get; set; }

        [Display(Name = "ZipCode", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string Zip { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "CityReq")]
        [Display(Name = "City", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string City { get; set; }

        public Int64 addressId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
