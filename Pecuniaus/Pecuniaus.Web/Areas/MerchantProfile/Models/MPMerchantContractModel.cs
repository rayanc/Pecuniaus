using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.MerchantProfile.Models
{
    [FluentValidation.Attributes.Validator(typeof(Pecuniaus.Validators.MPMerchantContractValidator))]
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

        [Display(Name = "ContactNumber", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contracts))]
        public string ContractNumber { get; set; }

        [Display(Name = "CreationDate", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contracts))]
        public DateTime CreationDate { get; set; }

        [Display(Name = "LoanedAmount", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contracts))]
        [DataType(DataType.Currency)]
        public decimal LoanedAmount { get; set; }

        [Display(Name = "OwnedAmount", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contracts))]
        [DataType(DataType.Currency)]
        public decimal OwnedAmount { get; set; }

        [Display(Name = "ContractStatus", ResourceType = typeof(Pecuniaus.Resources.Common))]
        public string ContractStatus { get; set; }

        [Display(Name = "PaidPercentage", ResourceType = typeof(Pecuniaus.Resources.Common))]
        public double PaidPercentage { get; set; }

        [Display(Name = "FundingDate", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contracts))]
        [DataType(DataType.Date)]
        public DateTime? FundingDate { get; set; }

        [Display(Name = "RetentionPercentage", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contracts))]
        public double RetentionPercentage { get; set; }

        [Display(Name = "RetentionChangeReason", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contracts))]
        public string RetentionChangeReason { get; set; }

        [Display(Name = "AdministrativeExpenses", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contracts))]
        public double AdministrativeExpenses { get; set; }


        [Display(Name = "Price", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contracts))]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Time", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contracts))]
        public double Time { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        [Display(Name = "RealTime", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contracts))]
        public double RealTime { get; set; }

        [Display(Name = "PaidAmount", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contracts))]
        [DataType(DataType.Currency)]
        public decimal PaidAmount { get; set; }

        [Display(Name = "PendingAmount", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contracts))]
        [DataType(DataType.Currency)]
        public decimal PendingAmount { get; set; }

        [Display(Name = "Score", ResourceType = typeof(Pecuniaus.Resources.Common))]
        public string Score { get; set; }

        public MPMerchantHistoryDetailModel HistoryDetail { get; set; }
        public MPMerchantActivityDetailModel ActivityDetail { get; set; }
        public List<MPMerchantContractSalesRepresentativeModel> SalesRep { get; set; }
    }
}