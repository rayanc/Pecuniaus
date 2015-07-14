using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Pecuniaus.Utilities.Validation;


namespace Pecuniaus.Web.Models
{
    public class MerchantsModel
    {
        public Int64 merchantId { get; set; }
        [Display(Name = "Merchant Name")]        
        public string merchantName { get; set; }
        [Display(Name = "Business Name")]
        [Required(ErrorMessage = "Please Enter Business Name")]
        public string businessName { get; set; }
        [Display(Name = "Legal Name")]
        [Required(ErrorMessage = "Please Enter Legal Name")]
        public string legalName { get; set; }
        public string ownerName { get; set; }
        [Display(Name = "RNC")]
        [Required(ErrorMessage = "Please Enter RNC")]
        [RegularExpression(ValidationRules.RNC, ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "RNCValidation")]
        public string rnc { get; set; }
        public string mccCode { get; set; }
        public int ownerId { get; set; }
        public Int16 isSynced { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Business Start Date")]
        [Required(ErrorMessage = "Please Enter Business Start Date")]
        public DateTime? businessStartDate { get; set; }
        public string businessWebSite { get; set; }
        public int rentFlag { get; set; }
        [Display(Name = "Business Type")]
        [Required(ErrorMessage = "Please Select Business Type")]
        public Int64 businessTypeId { get; set; }
        public double rentAmount { get; set; }
        public double annualSales { get; set; }
        [Display(Name = "MCC Code")]
        [Required(ErrorMessage = "Please Select MCC Code")]
        public Int32 industryTypeId { get; set; }
        public int propertyType { get; set; }
        public int processorCompany { get; set; }
        public int affiliationNumber { get; set; }
        public int CNetProcessorId { get; set; }
        public int VNetProcessoId { get; set; }
        public int salesRepId { get; set; }
        public int companyId { get; set; }
        public Address address { get; set; }
        public string OwnerAddress { get; set; }
        public ProcessorModel processor { get; set; }
        public IEnumerable<GeneralModel> IndustriesList { get; set; }
        public IEnumerable<GeneralModel> BusinessEntityList { get; set; }

        public string assignedSalesRep { get; set; }
        public string phoneNumber { get; set; }
        public string SSN { get; set; }
        public string TypeofBusinessentity { get; set; }
    }
}