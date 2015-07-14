using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantCreditReportModel
    {
        public int NumberofLoans { get; set; }
        [DataType(DataType.Currency)]
        public decimal ApprovedAmount { get; set; }
        [DataType(DataType.Currency)]
        public decimal OwnedAmount { get; set; }
        [DataType(DataType.Currency)]
        public decimal MonthlyPayment { get; set; }
        [DataType(DataType.Currency)]
        public decimal LateAmount { get; set; }
        [DataType(DataType.Currency)]
        public decimal AmountInLegal { get; set; }
        [DataType(DataType.Currency)]
        public decimal LateIndex { get; set; }
        [DataType(DataType.Currency)]
        public decimal DebtIndex { get; set; }
        public int AnalysisType { get; set; }
        public string ReportType { get; set; }
    }
}