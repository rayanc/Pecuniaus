using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantBusinessModel
    {
        public Int64 UserId { get; set; }
        [Display(Name = "MerchantID", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public Int64 MerchantID { get; set; }
        [Display(Name = "MerchantStatus", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string MerchantStatus { get; set; }
        [Display(Name = "ContractStartus", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string ContractStatus { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "LoanedAmount", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public decimal LoanedAmount { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "OwnedAmount", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public decimal OwnedAmount { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "PaidAmount", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public decimal PaidAmount { get; set; }

        [Display(Name = "ContractId", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public Int64 ContractId { get; set; }
        public MPMerchantBusinessInfoModel Business { get; set; } 
        public MPMerchantAddressInfoModel PhysicalAddress { get; set; }
        public MPMerchantAddressInfoModel LegalAddress { get; set; }
        public IEnumerable<MPMerchantProcessorInfoModel> Processor { get; set; }
    }
}