using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    
    public class FinalVerificationModel
    {

        public string bankName { get; set; }
        public string accountNumber { get; set; }
        public string accountName { get; set; }
        public double mcaAmount { get; set; }
        public double expenseAmount { get; set; }
        public double totalFundingAmount { get; set; }
        public long contractId { get; set; }
        public long merchantId { get; set; }
     
        public string legalFile { get; set; }
        public string legalFilePath { get; set; }
        public int documentTypeID { get; set; }
        public long documentId { get; set; }


        public string BLAFile { get; set; }
        public string BLAFilePath { get; set; }
        public int BLADocumentTypeId { get; set; }
        public long BLADocumentId { get; set; }

        public string action { get; set; }
      


    }
}