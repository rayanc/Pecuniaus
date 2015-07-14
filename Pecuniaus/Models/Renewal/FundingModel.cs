using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecuniaus.Models.Renewal
{
   public  class FundingModel
    {

        [Display(Name = "BankName", ResourceType = typeof(Resources.Renewal.Renewal))]
        [Required(ErrorMessageResourceName = "BankName", ErrorMessageResourceType = typeof(Resources.Renewal.Renewal))]
        public string BankName { get; set; }

        [Display(Name = "AccountNumber", ResourceType = typeof(Resources.Renewal.Renewal))]
        public string AccountNumber { get; set; }

        [Display(Name = "AccountName", ResourceType = typeof(Resources.Renewal.Renewal))]
        public string AccountName { get; set; }

        [Display(Name = "McaAmount", ResourceType = typeof(Resources.Renewal.Renewal))]
        public string McaAmount { get; set; }

        [Display(Name = "ExpenseAmount", ResourceType = typeof(Resources.Renewal.Renewal))]
        public string ExpenseAmount { get; set; }

        [Display(Name = "TotalOwnedAount", ResourceType = typeof(Resources.Renewal.Renewal))]
        public string TotalOwnedAount { get; set; }

        [Display(Name = "OwnerName", ResourceType = typeof(Resources.Renewal.Renewal))]
        public string OwnerName { get; set; }

        [Display(Name = "FundingComplete", ResourceType = typeof(Resources.Renewal.Renewal))]
        public bool FundingComplete { get; set; }//Funding Complete*

        [Display(Name = "WireTransfer", ResourceType = typeof(Resources.Renewal.Renewal))]
        public string WireTransfer { get; set; }//Upload proof of wireTransf
    }
}
