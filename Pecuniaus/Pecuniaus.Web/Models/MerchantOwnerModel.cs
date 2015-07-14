using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.Web.Models
{
    public class MerchantOwnerModel
    {
        public MerchantOwnerModel()
        {
            States = new List<SelectListItem>();
        }

        public IEnumerable<SelectListItem> States { get; set; }

        [Display(Name = "OwnerFirstName", ResourceType = typeof(Resources.Contract.DataEntry))]
        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "OwnerFirstNameRequired")]
        public string OwnerFirstName { get; set; }

        [Display(Name = "OwnerLastName", ResourceType = typeof(Resources.Contract.DataEntry))]
        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "OwnerLastNameRequired")]
        public string OwnerLastName { get; set; }

        public Int64 OwnerId { get; set; }
        public Int64 MerchanId { get; set; }

        [Required]
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string Phone1 { get; set; }

        [Required]
        [Display(Name = "CellNumber", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string CellNumber { get; set; }

        [Required]
        [Display(Name = "AddressLine1", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string AddressLine1 { get; set; }
        [Display(Name = "AddressLine2", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string AddressLine2 { get; set; }

        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string Email { get; set; }

        public string Ssn { get; set; }

        [Required]
        [Display(Name = "OwnerDOB", ResourceType = typeof(Resources.Contract.DataEntry))]
        [DataType(DataType.Date)]
        public DateTime OwnerDOB { get; set; }

        [Required]
        [Display(Name = "PassportNumber", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string PassportNumber { get; set; }

        [Required]
        [Display(Name = "Authorized", ResourceType = typeof(Resources.Contract.DataEntry))]
        public bool Authorized { get; set; }

        public string ZipId { get; set; }
        public string CityId { get; set; }

        [Required]
        [Display(Name = "States", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int StateID { get; set; }

        [Required]
        [Display(Name = "ZipCode", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string Zip { get; set; }

        [Required]
        [Display(Name = "City", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string City { get; set; }


    }
}