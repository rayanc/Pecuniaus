using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models.Merchant
{
    public class MerchantBankStatement
    {  
        public long StatementId { get; set; }      
        public long StatementMonthId { get; set; }
        public string StatementMonth { get; set; }
        public string StatementYear { get; set; }
        public double Amount { get; set; }
        public long MerchantId { get; set; }
        public long ContractId { get; set; }      
    }
}