using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Pecuniaus.Renewal.Models
{
    public class MerchantsDetail
    {
         [Display(Name = "merchantName", ResourceType = typeof(Resources.Renewal.Renewal))]
        public string merchantName { get; set; }
        [Display(Name = "businessName", ResourceType = typeof(Resources.Renewal.Renewal))]
        public string businessName { get; set; }
        [Display(Name = "merchantStatus", ResourceType = typeof(Resources.Renewal.Renewal))]
        public string merchantStatus { get; set; }
        public Int64 merchantId { get; set; }
        [Display(Name = "everInCollection", ResourceType = typeof(Resources.Renewal.Renewal))]
        public double everInCollection { get; set; }
        [Display(Name = "taskName", ResourceType = typeof(Resources.Renewal.Renewal))]
        public string taskName { get; set; }
        [Display(Name = "taskstatus", ResourceType = typeof(Resources.Renewal.Renewal))]
        public string taskstatus { get; set; }
        [Display(Name = "contractStatus", ResourceType = typeof(Resources.Renewal.Renewal))]
        public string contractStatus { get; set; }
        [Display(Name = "loanamount", ResourceType = typeof(Resources.Renewal.Renewal))]
        public double loanamount { get; set; }
        [Display(Name = "ownedAmount", ResourceType = typeof(Resources.Renewal.Renewal))]
        public double ownedAmount { get; set; }
        [Display(Name = "paidamount", ResourceType = typeof(Resources.Renewal.Renewal))]
        public double paidamount { get; set; }
        [Display(Name = "pendingAmount", ResourceType = typeof(Resources.Renewal.Renewal))]
        public double pendingAmount { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")] 
        [Display(Name = "fundedDate", ResourceType = typeof(Resources.Renewal.Renewal))]
        public DateTime ? fundedDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")] 
        [Display(Name = "expectedDate", ResourceType = typeof(Resources.Renewal.Renewal))]
        public DateTime expectedDate { get; set; }
        [Display(Name = "paidpercent", ResourceType = typeof(Resources.Renewal.Renewal))]
        public double paidpercent { get; set; }
        [Display(Name = "expectedturn", ResourceType = typeof(Resources.Renewal.Renewal))]
        public double expectedturn { get; set; }
        [Display(Name = "actualturn", ResourceType = typeof(Resources.Renewal.Renewal))]
        public double actualturn { get; set; }
        [Display(Name = "contractId", ResourceType = typeof(Resources.Renewal.Renewal))]
        public Int64 contractId { get; set; }
    }
}