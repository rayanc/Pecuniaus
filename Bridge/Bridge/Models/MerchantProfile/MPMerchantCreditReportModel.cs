using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantCreditReportModel
    {
        public int NumberofLoans { get; set; }
        public decimal ApprovedAmount { get; set; }
        public decimal OwnedAmount { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal LateAmount { get; set; }
        public decimal AmountInLegal { get; set; }
        public decimal LateIndex { get; set; }
        public decimal DebtIndex { get; set; }
        public int AnalysisType { get; set; }
        public string ReportType { get; set; }
    }
}