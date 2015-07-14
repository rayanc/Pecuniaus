using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.Renewal.Models
{
    public class FundingModel
    {


        public IEnumerable<SelectListItem> Banklist { get; set; }

        [Display(Name = "BankName", ResourceType = typeof(Resources.Renewal.Funding))]
        //[Required(ErrorMessageResourceName = "BankName", ErrorMessageResourceType = typeof(Resources.Renewal.Funding))]
        public string bankName { get; set; }

        [Required]
        [Display(Name = "AccountNumber", ResourceType = typeof(Resources.Renewal.Funding))]
        public string accountNumber { get; set; }

        [Required]
        [Display(Name = "AccountName", ResourceType = typeof(Resources.Renewal.Funding))]
        public string accountName { get; set; }

        [Required]
        [Display(Name = "McaAmount", ResourceType = typeof(Resources.Renewal.Funding))]
        [DataType(DataType.Currency)]
        public decimal mcaAmount { get; set; }

        [Required]
        [Display(Name = "ExpenseAmount", ResourceType = typeof(Resources.Renewal.Funding))]
        [DataType(DataType.Currency)]
        public decimal expenseAmount { get; set; }

        [Required]
        [Display(Name = "TotalOwnedAount", ResourceType = typeof(Resources.Renewal.Funding))]
        //[Range(typeof(Decimal), 1, 10000000, ErrorMessage = "Total Funding Amount must be greater than 0")]
        [DataType(DataType.Currency)]
        public decimal totalFundingAmount { get; set; }

        [Display(Name = "OwnerName", ResourceType = typeof(Resources.Renewal.Funding))]
        public string ownerName { get; set; }


        [Display(Name = "WireTransfer", ResourceType = typeof(Resources.Renewal.Funding))]
        public string wireTransfer { get; set; }//Upload proof of wireTransf

        [Display(Name = "ContractReviewed", ResourceType = typeof(Resources.Renewal.Funding))]
        public bool contractReviewed { get; set; }

        [Display(Name = "ContractFunded", ResourceType = typeof(Resources.Renewal.Funding))]
        public bool contractFunded { get; set; }

        [Required]
        public int bankId { get; set; }
        public int ownerId { get; set; }
        public int ContractId { get; set; }
        public int MerchantId { get; set; }
        public int documentTypeID { get; set; }
        public long documentId { get; set; }
        public string fileName { get; set; }
        public string FileDetails { get; set; }
        public string filePath { get; set; }
        public string action { get; set; }
        public int contractStatusId { get; set; }
        public long UserId { get; set; }
        public string ReviewedBy { get; set; }
        public string CompletedBy { get; set; }

    }
}