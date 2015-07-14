using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class SearchFinanceModel
    {
        public Int64? merchantId { get; set; }
        public int? activityTypeId { get; set; }
        public string dateOfActivity { get; set; }
        public Int64? affiliationId { get; set; }
        public int? processorId { get; set; }
        public decimal amount { get; set; }
        public string notes { get; set; }
        public Int64 insertUserId { get; set; }
        public Int64? contractId { get; set; }
        public Int64 transferContractId { get; set; }
        public Int64? contractTypeId { get; set; }
        public decimal pendingAmount { get; set; }
        public Int64? processorTypeId { get; set; }
    }

    public class FinanceModel
    {
        public DateTime dateOfActivity { get; set; }
        public string processorName { get; set; }
        public double retention { get; set; }
        public decimal totalAmount { get; set; }
        public decimal price { get; set; }
        public decimal capital { get; set; }
        public decimal incomeThroughProcessor { get; set; }
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
        public Int64 merchantId { get; set; }        
        public int processorId { get; set; }
        public Int64 contractId { get; set; }  
        public decimal TransferAmount { get; set; }
        public decimal pendingAmount { get; set; }
        public Int64? contractTypeId { get; set; }
        
    }
}
