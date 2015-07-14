using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantCreditReportDetailModel
    {
        public MPMerchantCreditReportDetailModel()
        {
            AllContracts = new List<SelectListItem>();
            AllContractCreditReports = new List<SelectListItem>();
        }
        public Int64 ContractId { get; set; }
        public int ReportId { get; set; }
        public MPMerchantCreditReportModel Total_TotalCredit { get; set; }
        public MPMerchantCreditReportModel Total_Loans { get; set; }
        public MPMerchantCreditReportModel Total_CreditCards { get; set; }
        public MPMerchantCreditReportModel Total_Others { get; set; }
        public MPMerchantCreditReportModel Business_TotalCredit { get; set; }
        public MPMerchantCreditReportModel Business_Loans { get; set; }
        public MPMerchantCreditReportModel Business_CreditCards { get; set; }
        public MPMerchantCreditReportModel Business_Others { get; set; }
        public MPMerchantCreditReportModel Owner_TotalCredit { get; set; }
        public MPMerchantCreditReportModel Owner_Loans { get; set; }
        public MPMerchantCreditReportModel Owner_CreditCards { get; set; }
        public MPMerchantCreditReportModel Owner_Others { get; set; }
        public IEnumerable<SelectListItem> AllContracts { get; set; }
        public IEnumerable<SelectListItem> AllContractCreditReports { get; set; }
    }
}