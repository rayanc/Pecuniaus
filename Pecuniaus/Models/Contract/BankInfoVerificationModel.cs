using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pecuniaus.Models.Contract
{
    public class BankInfoVerificationModel
    {

        public BankInfoVerificationModel()
        {
            Banks = new List<SelectListItem>();
        }
        public long BankDetailId { get; set; }
        public IEnumerable<SelectListItem> Banks { get; set; }

        [Display(Name = "BankName", ResourceType = typeof(Resources.Contract.BankVerification))]
        [Required]
        public int BankID { get; set; }

        [Display(Name = "AccountNumber", ResourceType = typeof(Resources.Contract.BankVerification))]
        [Required]
        public string AccountNumber { get; set; }

        [Display(Name = "BankCode", ResourceType = typeof(Resources.Contract.BankVerification))]
        [Required]
        public string BankCode { get; set; }

        [Display(Name = "AccountName", ResourceType = typeof(Resources.Contract.BankVerification))]
        [Required]
        public string AccountName { get; set; }
        public long DocTypeId { get; set; }
        public string FileName { get; set; }
        public string FileDetails { get; set; }
        public long merchantId { get; set; }
        public long contractId { get; set; }
    }
}
