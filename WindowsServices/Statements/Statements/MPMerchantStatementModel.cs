using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statements
{
    public class MPMerchantStatementModel
    {
        public Int64 MerchantId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ProcessorName { get; set; }
        public int TotalTransaction { get; set; }
        public double RetentionPercentage { get; set; }
        public double PaymentsReceived { get; set; }
    }
}