using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.Web.Models
{
    public class MerchantDataEntryModel
    {
        public MerchantDataEntryModel()
        {

            Provinces = new List<SelectListItem>();
            TypeOfProperties = new List<SelectListItem>();
            IndustryTypes = new List<SelectListItem>();
            Cities = new List<SelectListItem>();
            States = new List<SelectListItem>();
            ProcessorCompar = new List<SelectListItem>();
        }
        public IEnumerable<SelectListItem> Provinces { get; set; }

        public IEnumerable<SelectListItem> TypeOfProperties { get; set; }

        public IEnumerable<SelectListItem> IndustryTypes { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }

        public IEnumerable<SelectListItem> ProcessorCompar { get; set; }


        [Display(Name = "BusinessName", ResourceType = typeof(Resources.Contract.DataEntry))]
        [Required]
        public string BusinessName { get; set; }

        [Required]
        [Display(Name = "CompanyName", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "RNC", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string RNC { get; set; }

        [Required]
        [Display(Name = "Address", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string Address { get; set; }

        [Required]
        [Display(Name = "City", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string City { get; set; }

        [Required]
        [Display(Name = "TelephoneNumber", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string TelephoneNumber { get; set; }

        [Required]
        [Display(Name = "OtherTelephoneNumber", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string OtherTelephoneNumber { get; set; }

        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string Email { get; set; }

        [Required]
        [Display(Name = "RentAmount", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string RentAmount { get; set; }

        [Required]
        [Display(Name = "BusinessStartDate", ResourceType = typeof(Resources.Contract.DataEntry))]
        [DataType(DataType.Date)]
        public DateTime BusinessStartDate { get; set; }

        [Required]
        [Display(Name = "GrossYearlySale", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string GrossYearlySale { get; set; }

        [Required]
        [Display(Name = "LoanAmountRequired", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string LoanAmountRequired { get; set; }

        [Required]
        [Display(Name = "AffiliateNumber", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string AffiliateNumber { get; set; }

        [Required]
        [Display(Name = "FirstProcessedDate", ResourceType = typeof(Resources.Contract.DataEntry))]
        [DataType(DataType.Date)]
        public DateTime FirstprocessedDate { get; set; }

        [Required]
        [Display(Name = "Provinces", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int ProvinceID { get; set; }

        [Required]
        [Display(Name = "PropertyTypes", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int TypeOfPropertyID { get; set; }

        [Required]
        [Display(Name = "IndustryTypes", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int IndustryTypeID { get; set; }

        [Required]
        [Display(Name = "Cities", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int CityID { get; set; }

        [Required]
        [Display(Name = "States", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int StateID { get; set; }

        [Required]
        [Display(Name = "ProcessorCompar", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int ProcessorComparID { get; set; }

        [Required]
        [Display(Name = "TypeOfBusinessEntity", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string TypeofBusinessentity { get; set; }

        public List<MerchantOwnerModel> owners { get; set; }

        public List<ProcessorModel> processor { get; set; }
  
    }
}