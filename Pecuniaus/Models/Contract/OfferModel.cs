using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecuniaus.Models.Contract
{
    public class OfferModel
    {
        public bool IsUpdate { get; set; }
        public int Id { get; set; }
        public Int64 offerId { get; set; }
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
      
        [Display(Name = "Time")]
        public decimal turn { get; set; }
      
        [Display(Name = "Monthly Payment")]
        [DataType(DataType.Currency)]
        public decimal monthlypayment { get; set; }
        
        [Display(Name = "Yearly Payment")]
        [DataType(DataType.Currency)]
        public decimal yearly { get; set; }
        
        [Display(Name = "MCA Amount")]
        [DataType(DataType.Currency)]
        public decimal loanAmount { get; set; }
        
        [Display(Name = "Owed Amount")]
        [DataType(DataType.Currency)]
        public decimal ownedAmount { get; set; }
        
        [Display(Name = "Sales Taken")]
        public double salestaken { get; set; }

        [Display(Name = "Proportion")]
        [Range(1,2.1)]
        public decimal proportion { get; set; }
        public decimal retention { get; set; }
        public DateTime offerCreationDate { get; set; }
        public DateTime offerAcceptanceDate { get; set; }
        public Int64 insertuserId { get; set; }
        public DateTime offerexpirationDate { get; set; }
        
        [Required(ErrorMessage = "Please select offer")]
        public bool IsSelected { get; set; }
        public string Selectedoffer { get; set; }
        public double MaxAnnualSalesAmount { get; set; }
        public double MaxOwnedAmount { get; set; }
        public long maxprice { get; set; }
     
        public decimal TurnSl { get { return turn; } }
        public decimal RetentionSl { get { return retention; } }
        public decimal ProportionSL { get { return proportion; } }
        public decimal LoanAmountSL { get { return loanAmount; } }
    
        public bool IsEmailSent { get; set; }
    
    }
}
