using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bridge.Models
{
    public class MPMerchantBusinessModel
    {
        public MPMerchantBusinessModel()
        {
            Business = new MPMerchantBusinessInfoModel();
            PhysicalAddress = new MPMerchantAddressInfoModel();
            LegalAddress = new MPMerchantAddressInfoModel();
            Processor = new List<MPMerchantProcessorInfoModel>();
            IndustryTypes = new List<SelectListItem>();
            Processors = new List<SelectListItem>();
        }
        public Int64 UserId { get; set; }
        public Int64 MerchantID { get; set; }
        public Int64 ContractID { get; set; }
        public string MerchantStatus { get; set; }
        public string ContractStatus { get; set; }
        public double LoanedAmount { get; set; }
        public double OwnedAmount { get; set; }
        public double PaidAmount { get; set; }
        public MPMerchantBusinessInfoModel Business { get; set; } 
        public MPMerchantAddressInfoModel PhysicalAddress { get; set; }
        public MPMerchantAddressInfoModel LegalAddress { get; set; }
        public List<MPMerchantProcessorInfoModel> Processor { get; set; }
        public IEnumerable<SelectListItem> IndustryTypes { get; set; }
        public IEnumerable<SelectListItem> Processors { get; set; }
    }
}