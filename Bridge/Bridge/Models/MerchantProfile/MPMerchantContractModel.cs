using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantContractModel
    {
        public MPMerchantContractModel()
        {
            HistoryDetail = new MPMerchantHistoryDetailModel();
            ActivityDetail = new MPMerchantActivityDetailModel();
            SalesRep = new List<MPMerchantContractSalesRepresentativeModel>();
        }
        public Int64 UserId { get; set; }
        public Int64 ContractId { get; set; }
        public string ContractNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal LoanedAmount { get; set; }
        public decimal OwnedAmount { get; set; }
        public string ContractStatus { get; set; }
        public double PaidPercentage { get; set; }
        public DateTime? FundingDate { get; set; }
        public double RetentionPercentage { get; set; }
        public string RetentionChangeReason { get; set; }
        public double AdministrativeExpenses { get; set; }
        public decimal Price { get; set; }
        public double Time { get; set; }
        public double RealTime { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal PendingAmount { get; set; }
        public string Score { get; set; }
        public MPMerchantHistoryDetailModel HistoryDetail { get; set; }
        public MPMerchantActivityDetailModel ActivityDetail { get; set; }
        public List<MPMerchantContractSalesRepresentativeModel> SalesRep { get; set; }
    }
}