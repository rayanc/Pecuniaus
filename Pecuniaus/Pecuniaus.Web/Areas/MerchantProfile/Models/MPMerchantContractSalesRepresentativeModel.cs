using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantContractSalesRepresentativeModel
    {
        public Int64 UserId { get; set; }
        public Int64 ContractId { get; set; }
        [Display(Name = "SaleRepID", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contracts))]
        public int SalesRepId { get; set; }
        public string SalesRepName { get; set; }
        public string SaleType { get; set; }
        public int AgreementType { get; set; }
        public double Commission { get; set; }
        public double RenewalCommission { get; set; }
        [Display(Name = "IsPrimary", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Contracts))]
        public bool IsPrimary { get; set; }
        public IEnumerable<SelectListItem> AllSalesReps { get; set; }
    }
}