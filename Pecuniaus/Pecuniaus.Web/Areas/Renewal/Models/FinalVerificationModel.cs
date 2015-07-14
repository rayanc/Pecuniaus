using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.Renewal.Models
{
    public class FinalVerificationModel
    {
        [Display(Name = "BankName", ResourceType = typeof(Resources.Contract.FinalVerification))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Contract.FinalVerification), ErrorMessageResourceName = "BankNameReq")]
        public string bankName { get; set; }

        [Display(Name = "BankAccountNumber", ResourceType = typeof(Resources.Contract.FinalVerification))]
        public string accountNumber { get; set; }

        [Display(Name = "BankAccountName", ResourceType = typeof(Resources.Contract.FinalVerification))]
        public string accountName { get; set; }

        [Display(Name = "McaAmount", ResourceType = typeof(Resources.Contract.FinalVerification))]
        public double mcaAmount { get; set; }

        [Display(Name = "AdministrativeExpenses", ResourceType = typeof(Resources.Contract.FinalVerification))]
        public double expenseAmount { get; set; }

        [Display(Name = "TotalFundingAmount", ResourceType = typeof(Resources.Contract.FinalVerification))]
        public double totalFundingAmount { get; set; }

        
        public long contractId { get; set; }
        public long merchantId { get; set; }
        public string action { get; set; }
        
        public string legalFile { get; set; }
        public string legalFilePath { get; set; }        
        public int documentTypeID { get; set; }
        public long documentId { get; set; }

        public string BLAFile { get; set; }
        public string BLAFilePath { get; set; }
        public int BLADocumentTypeId { get; set; }
        public long BLADocumentId { get; set; }
    }
}