using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pecuniaus.Models.Contract
{
    public class BankStatementModel:BaseModel
    {
        public int Id { get; set; }
        public long StatementId { get; set; }
        
        [Required]
        [Display(Name = "Month", ResourceType = typeof(Resources.Contract.BankStatement))]
        public int StatementMonthId { get; set; }
     
        [Display(Name = "Month", ResourceType = typeof(Resources.Contract.BankStatement))]
        public string StatementMonth { get; set; }
        
        [Required]
        [Display(Name = "Year", ResourceType = typeof(Resources.Contract.BankStatement))]
        public string StatementYear { get; set; }
    
        [Required]
        [Display(Name = "Amount", ResourceType = typeof(Resources.Contract.BankStatement))]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public long MerchantId { get; set; }
        public long ContractId { get; set; }
        public IEnumerable<SelectListItem> StatementMonths { get; set; }
        public IEnumerable<SelectListItem> StatementYears { get; set; }
    }
}
