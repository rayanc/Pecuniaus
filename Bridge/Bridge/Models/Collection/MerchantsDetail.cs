using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MerchantsDetail
    {
        public string merchantName { get; set; }
        public Int64 merchantId { get; set; }
        public bool everInCollection { get; set; }
        public string taskName { get; set; }
        public string taskstatus { get; set; }
        public string merchantStatus { get; set; }
        public double loanamount { get; set; }
        public double ownedAmount { get; set; }
        public double paidamount { get; set; }
        public double pendingAmount { get; set; }
        public DateTime fundedDate { get; set; }
        public DateTime expectedDate { get; set; }
        public double paidpercent { get; set; }
        public double expectedturn { get; set; }
        public double actualturn { get; set; }
        public Int64 contractId { get; set; }
    }
}