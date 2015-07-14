using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Pecuniaus.Utilities.Validation;


namespace Pecuniaus.Web.Models
{
    public class DataEntryModel
    {
        public DataEntryModel()
        {

            Provinces = new List<SelectListItem>();
            TypeOfProperties = new List<SelectListItem>();
            SalesReps = new List<SelectListItem>();
            IndustryTypes = new List<SelectListItem>();
            Cities = new List<SelectListItem>();
            States = new List<SelectListItem>();
            ProcessorCompar = new List<SelectListItem>();
            BusinessType = new List<SelectListItem>();
            address = new AddressModel();
        }
        public IEnumerable<SelectListItem> Provinces { get; set; }
        public IEnumerable<SelectListItem> TypeOfProperties { get; set; }
        public IEnumerable<SelectListItem> BusinessType { get; set; }
        public IEnumerable<SelectListItem> IndustryTypes { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
        public IEnumerable<SelectListItem> ProcessorCompar { get; set; }
        public IEnumerable<SelectListItem> SalesReps { get; set; }


        [Display(Name = "BusinessName", ResourceType = typeof(Resources.Contract.DataEntry))]
        [Required]
        public string businessName { get; set; }

        [Required]
        // [Display(Name = "CompanyName", ResourceType = typeof(Resources.Contract.DataEntry))]
        [Display(Name = "Legal Name")]
        public string legalName { get; set; }

        [Required]
        [Display(Name = "RNC", ResourceType = typeof(Resources.Contract.DataEntry))]
        [RegularExpression(ValidationRules.RNC, ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "RNCValidation")]
        public string rnc { get; set; }

        public AddressModel address { get; set; }

        [Required]
        [Display(Name = "RentAmount", ResourceType = typeof(Resources.Contract.DataEntry))]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        [DataType(DataType.Currency)]
        public decimal rentAmount { get; set; }

        [Required]
        [Display(Name = "BusinessStartDate", ResourceType = typeof(Resources.Contract.DataEntry))]
        [DataType(DataType.Date)]
        public DateTime? businessStartDate { get; set; }

        [Required]
        [Display(Name = "GrossYearlySale", ResourceType = typeof(Resources.Contract.DataEntry))]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        [DataType(DataType.Currency)]
        public decimal annualSales { get; set; }

        [Required]
        [Display(Name = "LoanAmountRequired", ResourceType = typeof(Resources.Contract.DataEntry))]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        [DataType(DataType.Currency)]
        public Decimal loanAmountRequired { get; set; }
        
        [Display(Name = "AffiliateNumber", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string affiliationNumber { get; set; }

        [Required]
        [Display(Name = "FirstProcessedDate", ResourceType = typeof(Resources.Contract.DataEntry))]
        [DataType(DataType.Date)]
        public DateTime firstProcessedDate { get; set; }

        [Required]
        [Display(Name = "PropertyTypes", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int propertyType { get; set; }

        [Required]
        [Display(Name = "IndustryTypes", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int industryTypeId { get; set; }

        [Required]
        [Display(Name = "Cities", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int CityID { get; set; }

        [Required]
        [Display(Name = "ProcessorCompar", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int ProcessorComparID { get; set; }

        [Required]
        [Display(Name = "TypeOfBusinessEntity", ResourceType = typeof(Resources.Contract.DataEntry))]
        public Int64 businessTypeId { get; set; }
        public Int64 addressId { get; set; }
        public Int64 contractId { get; set; }
        public List<OwnerModel> Owners { get; set; }

        [Required]
        [Display(Name = "Primary Sales Rep")]
        public int PsalesRepId { get; set; }
        [Display(Name = "Secondary Sales Rep")]
        public int? SecsalesRepId { get; set; }
        public List<ProcessorModel> Processor { get; set; }

    }
}