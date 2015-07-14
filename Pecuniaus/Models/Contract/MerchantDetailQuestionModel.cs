using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pecuniaus.Models.Contract
{

    public class MerchantDetailQuestionModel
    {
        public long MerchantId { get; set; }
      
        [Required]
        public string LegalName { get; set; }

        [Required]
        public string BusinessName { get; set; }
        //public string SalesRepName { get; set; }

        [Required]
        public string OwnerFirstName { get; set; }
      
        public string OwnerLastName { get; set; }    
        
        public int IsAuthorised { get; set; }
        
        [DataType(DataType.Currency)]
        public long RetensionPct { get; set; }
        
        [DataType(DataType.Currency)]
        public double LoanAmount { get; set; }
        
       // [DataType(DataType.Currency)]
        public double OwnedAmount { get; set; }
    
        [DataType(DataType.Currency)]
        public double GrossAnnualSales { get; set; }
        
        [DataType(DataType.Currency)]
        public decimal AdminExp { get; set; }

        [Required]
        public string OwnerPassport { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime BusinessStartDate { get; set; }
        public string MerchantAddress { get; set; }
        public string LandlordAddress { get; set; }
        public double? RentedAmount { get; set; }
        public string LLFirstName { get; set; }
        public string LLLastName { get; set; }
        public long? TypeOfAdvanceId { get; set; }

    }

}
