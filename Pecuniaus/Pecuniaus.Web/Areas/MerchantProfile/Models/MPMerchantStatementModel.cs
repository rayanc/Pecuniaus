using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantStatementModel
    {
        public DateTime TransactionDate { get; set; }
        public string ProcessorName { get; set; }
        public string TotalTransaction { get; set; }
        public double RetentionPercentage { get; set; }
        public string PaymentsReceived { get; set; }
    }
}