using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Pecuniaus.Finance.Models
{
    public class SearchFinanceModel
    {

        [Display(Name = "MerchantId", ResourceType = typeof(Pecuniaus.Resources.Finance.Finance))]
        public Int64? merchantId { get; set; }

        [Display(Name = "ActivityType", ResourceType = typeof(Pecuniaus.Resources.Finance.Finance))]
        public int? activityTypeId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "DateOfActivity", ResourceType = typeof(Pecuniaus.Resources.Finance.Finance))]
        public DateTime? dateOfActivity { get; set; }

        [Display(Name = "AffiliationNo", ResourceType = typeof(Pecuniaus.Resources.Finance.Finance))]
        public Int64? affiliationId { get; set; }

        [Display(Name = "ProcessorCompany", ResourceType = typeof(Pecuniaus.Resources.Finance.Finance))]
        public int? processorId { get; set; }
        [DataType(DataType.Currency)]
        public decimal amount { get; set; }
        public string notes { get; set; }
        public Int64 insertUserId { get; set; }
        public Int64? contractId { get; set; }       
        public IEnumerable<SelectListItem> activityType { get; set; }
        public IEnumerable<SelectListItem> processorCompany { get; set; }
        public IEnumerable<FinanceModel> finance { get; set; }
        public AddFinanceModel addFinance { get; set; }
    }

    public class FinanceModel
    {
        private DateTime dtVal;
        public string dateOfActivity
        {
            get { return dtVal.ToShortDateString(); }
            set { dtVal = Convert.ToDateTime(value); }
        }

        public string processorName { get; set; }
        public double retention { get; set; }
        [DataType(DataType.Currency)]
        public decimal totalAmount { get; set; }
        [DataType(DataType.Currency)]
        public decimal price { get; set; }
        [DataType(DataType.Currency)]
        public decimal capital { get; set; }
        [DataType(DataType.Currency)]
        public decimal incomeThroughProcessor { get; set; }
        [DataType(DataType.Currency)]
        public decimal otherIncome { get; set; }
        public string activityType { get; set; }
        public string contractNumber { get; set; }
        public string notes { get; set; }
        public long ActivityId { get; set; }
    }

    public class FinanceDD
    {
        public int keyId { get; set; }
        public string value { get; set; }
    }

    public class AddFinanceModel
    {
        [Required]        
        [Display(Name = "MerchantId", ResourceType = typeof(Pecuniaus.Resources.Finance.Finance))]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Count must be a natural number")]
        public Int64 merchantId { get; set; }

        [Required]
        [Display(Name = "ActivityType", ResourceType = typeof(Pecuniaus.Resources.Finance.Finance))]
        public int activityTypeId { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "DateOfActivity", ResourceType = typeof(Pecuniaus.Resources.Finance.Finance))]
        public DateTime? dateOfActivity { get; set; }
        public Int64? affiliationId { get; set; }

        [Required]
        [Display(Name = "ProcessorCompany", ResourceType = typeof(Pecuniaus.Resources.Finance.Finance))]
        public int processorId { get; set; }

        [Required]
        [Display(Name = "Amount", ResourceType = typeof(Pecuniaus.Resources.Finance.Finance))]
        [DataType(DataType.Currency)]
        public decimal amount { get; set; }


        [Display(Name = "Notes", ResourceType = typeof(Pecuniaus.Resources.Finance.Finance))]
        public string notes { get; set; }
        public Int64 insertUserId { get; set; }

        [Required]
        [Display(Name = "ContractId", ResourceType = typeof(Pecuniaus.Resources.Finance.Finance))]
        public Int64 contractId { get; set; }
        public IEnumerable<FinanceModel> finance { get; set; }
        public IEnumerable<SelectListItem> activityType { get; set; }
        public IEnumerable<SelectListItem> processorCompany { get; set; }

        [Display(Name = "transferContractId", ResourceType = typeof(Pecuniaus.Resources.Finance.Finance))]
        public Int64 transferContractId { get; set; }
        
        [DataType(DataType.Currency)]       
        [Display(Name = "TransferAmount", ResourceType = typeof(Pecuniaus.Resources.Finance.Finance))]
        public decimal TransferAmount { get; set; }

        [Display(Name = "pendingAmount", ResourceType = typeof(Pecuniaus.Resources.Finance.Finance))]        
        [DataType(DataType.Currency)]
        public decimal pendingAmount { get; set; }
        public Int64? contractTypeId { get; set; }
        
    }
}