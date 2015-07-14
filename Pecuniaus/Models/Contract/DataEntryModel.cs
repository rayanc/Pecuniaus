using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Pecuniaus.Utilities.Validation;
using Pecuniaus.Validations;

namespace Pecuniaus.Models.Contract
{
    public class DataEntryModel : BaseModel
    {
        public DataEntryModel()
        {

            Provinces = new List<SelectListItem>();
            SalesReps = new List<SelectListItem>();
            TypeOfProperties = new List<SelectListItem>();
            TypeOfAdvances = new List<SelectListItem>();
            IndustryTypes = new List<SelectListItem>();
            Cities = new List<SelectListItem>();
            States = new List<SelectListItem>();
            ProcessorCompar = new List<SelectListItem>();
            Address = new AddressModel();
            Banks = new List<SelectListItem>();
            LandlordInformation = new LandlordInformationModel();
        }
        public IEnumerable<SelectListItem> Provinces { get; set; }
        public IEnumerable<SelectListItem> TypeOfProperties { get; set; }
        public IEnumerable<SelectListItem> IndustryTypes { get; set; }
        public IEnumerable<SelectListItem> StatementMonths { get; set; }
        public IEnumerable<SelectListItem> StatementYears { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
        public IEnumerable<SelectListItem> ProcessorCompar { get; set; }
        public IEnumerable<SelectListItem> BusinessType { get; set; }
        public IEnumerable<SelectListItem> SalesReps { get; set; }
        public IEnumerable<SelectListItem> TypeOfAdvances { get; set; }
        public IEnumerable<SelectListItem> Banks { get; set; }
        public long BusinessTypeId { get; set; }
        public long ContractId { get; set; }

        [Display(Name = "BusinessName", ResourceType = typeof(Resources.Contract.DataEntry))]
        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "BusinessNameReq")]
        public string BusinessName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "LegalNameReq")]
        [Display(Name = "LegalName", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string LegalName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "RNCReq")]
        [Display(Name = "RNC", ResourceType = typeof(Resources.Contract.DataEntry)), RegularExpression(ValidationRules.RNC, ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "RNCValidation")]
        public string RNC { get; set; }

        public AddressModel Address { get; set; }

        // [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "RentAmountReq")]
        [Display(Name = "RentAmount", ResourceType = typeof(Resources.Contract.DataEntry))]
        ///Required if PropteryTpe = Rented
        [RequiredIf("PropertyType == 200001", ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "RentAmountReq")]
        [DataType(DataType.Currency)]
        public decimal RentAmount { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "BusinessStartDateReq")]
        [Display(Name = "BusinessStartDate", ResourceType = typeof(Resources.Contract.DataEntry))]
        [DataType(DataType.Date)]
        public DateTime? BusinessStartDate { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "GrossYearlySaleReq")]
        [Display(Name = "GrossYearlySale", ResourceType = typeof(Resources.Contract.DataEntry))]
        [DataType(DataType.Currency)]
        public decimal GrossYearlySale { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "LoanAmountReq")]
        [Display(Name = "LoanAmountRequired", ResourceType = typeof(Resources.Contract.DataEntry))]
        [DataType(DataType.Currency)]
        public decimal LoanAmountRequired { get; set; }

        [Display(Name = "AffiliateNumber", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string AffiliationNumber { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "FirstProcessedDateReq")]
        [Display(Name = "FirstProcessedDate", ResourceType = typeof(Resources.Contract.DataEntry))]
        [DataType(DataType.Date)]
        public DateTime? FirstprocessedDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "PropertyTypesReq")]
        [Display(Name = "PropertyTypes", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int PropertyType { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "IndustryTypesReq")]
        [Display(Name = "IndustryTypes", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int IndustryTypeId { get; set; }
        [Required]
        [Display(Name = "Primary Sales Rep")]
        public int PsalesRepId { get; set; }
        [Display(Name = "Secondary Sales Rep")]
        public int? SecsalesRepId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "CitiesReq")]
        [Display(Name = "Cities", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int CityID { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "ProcessorComparReq")]
        [Display(Name = "ProcessorCompar", ResourceType = typeof(Resources.Contract.DataEntry))]
        public int ProcessorComparID { get; set; }

        // [Required]
        [Display(Name = "TypeOfBusinessEntity", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string TypeofBusinessentity { get; set; }

        [Display(Name = "BankAccountName", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string AccountName { get; set; }

        [Display(Name = "BankAccountNumber", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string AccountNumber { get; set; }

        public List<OwnerModel> Owners { get; set; }

        public List<ProcessorModel> Processor { get; set; }

        public List<TradeReferenceModel> TradeReference { get; set; }

        public List<BankStatementModel> BankStatements { get; set; }

        [Required(ErrorMessageResourceName = "AnnualSalesReq", ErrorMessageResourceType = typeof(Resources.Contract.DataEntry))]
        [Display(Name = "AnnualSales", ResourceType = typeof(Resources.Contract.DataEntry))]
        [DataType(DataType.Currency)]
        public decimal AnnualSales { get; set; }

        [Display(Name = "AnnualSalesFile", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string AnnualSalesCalcFile { get; set; }

        public string BankCode { get; set; }
        [Display(Name = "BankName", ResourceType = typeof(Resources.Contract.BankVerification))]
        [Required]
        public int BankID { get; set; }
        // [Required(ErrorMessageResourceType = typeof(Resources.Contract.DataEntry), ErrorMessageResourceName = "TypeOfAdvanceErr")]
        [Display(Name = "TypeOfAdvance", ResourceType = typeof(Resources.Contract.DataEntry))]
        public long? TypeOfAdvanceId { get; set; }


        public long LandlordId { get; set; }

        [RequiredIf("PropertyType == 200001", ErrorMessageResourceName = "LandlordCompanyNameReq", ErrorMessageResourceType = typeof(Resources.Contract.DataEntry))]
        [Display(Name = "LandlordCompanyName", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string CompanyName { get; set; }

        [RequiredIf("PropertyType == 200001", ErrorMessageResourceName = "LandlordFirstNameReq", ErrorMessageResourceType = typeof(Resources.Contract.DataEntry))]
        [Display(Name = "LandlordFirstName", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string FirstName { get; set; }

        [RequiredIf("PropertyType == 200001", ErrorMessageResourceName = "LandlordLastNameReq", ErrorMessageResourceType = typeof(Resources.Contract.DataEntry))]
        [Display(Name = "LandlordLastName", ResourceType = typeof(Resources.Contract.DataEntry))]
        public string LastName { get; set; }

        [RequiredIf("PropertyType == 200001", ErrorMessageResourceName = "LandlordPhoneNumberReq", ErrorMessageResourceType = typeof(Resources.Contract.DataEntry))]
        [Display(Name = "LandlordPhoneNumber", ResourceType = typeof(Resources.Contract.DataEntry))]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [RequiredIf("PropertyType == 200001")]
        public LandlordInformationModel LandlordInformation { get; set; }
    }
}
