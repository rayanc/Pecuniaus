using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantCreditReportDetailModel
    {
        public MPMerchantCreditReportDetailModel()
        {
            Total_TotalCredit = new MPMerchantCreditReportModel();
            Total_Loans = new MPMerchantCreditReportModel();
            Total_CreditCards = new MPMerchantCreditReportModel();
            Total_Others = new MPMerchantCreditReportModel();
            Business_TotalCredit = new MPMerchantCreditReportModel();
            Owner_TotalCredit = new MPMerchantCreditReportModel();
        }
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
    }
}