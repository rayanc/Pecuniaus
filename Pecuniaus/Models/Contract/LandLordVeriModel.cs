using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pecuniaus.Models.Contract
{
    public class LandLordVeriModel : BaseModel
    {
        public LlMerchantDetailModel MerchantDetails { get; set; }
        public IEnumerable<QuestionModel> Questions { get; set; }
  
        [Display(Name = "ScriptFile", ResourceType = typeof(Resources.Contract.VerificationCall))]
        public string ScriptFile { get; set; }
        public string ScriptFilePath { get; set; }
        public string UserFullName { get; set; }
        public LLRightWrong Answers { get; set; }
        public string RndStr
        {
            get
            {
                return new Random().Next().ToString();
            }
        }
    }

    public class LLRightWrong
    {
        public bool OwnerName { get; set; }
        public bool MerchantAddress { get; set; }
        public bool LandlordName { get; set; }
        public bool ContractStartDate { get; set; }
        public bool ContractAutoRenew { get; set; }
        public bool ContractExpiryDate{ get; set; }
        public bool ContractRenewAtEnd { get; set; }
        public bool RentedAmount { get; set; }
        public bool ContinueRent { get; set; }
        public bool AgreeRentLate { get; set; }
        public bool LessesBusiness { get; set; }
        public bool CurrentPayments { get; set; }
        public bool LLCompany { get;set; }

        public bool LegalName { get; set; }
        
    }

    public class LlMerchantDetailModel
    {
        public long MerchantId { get; set; }
        [Required]
        public string LegalName { get; set; }        
        [Required]
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }

        [DataType(DataType.Currency)]
        public decimal OwedAmount { get; set; }
        
        [DataType(DataType.Currency)]
        public long RetensionPct { get; set; }

        [DataType(DataType.Currency)]
        public decimal LoanAmount { get; set; }

        [DataType(DataType.Currency)]
        public decimal OwnedAmount { get; set; }
  
        [DataType(DataType.Currency)]
        public decimal GrossAnnualSales { get; set; }
        
        [DataType(DataType.Currency)]
        public decimal AdminExp { get; set; }
        public string MerchantAddress { get; set; }
        public string LandlordAddress { get; set; }
        public decimal? RentedAmount { get; set; }
        public string LLFirstName { get; set; }
        public string LLLastName { get; set; }
        public string LLCompany { get; set; }
        public long RentFlag { get; set; }      
        
        [DataType(DataType.Date)]
        public DateTime ContractStartDate{ get; set; }
        
        [DataType(DataType.Date)]
        public DateTime ContractExpireDate { get; set; }

        public int? PayMonth{ get; set; }
    
    }
}
