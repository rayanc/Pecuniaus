using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class Funding
    {        
        
        public string bankName { get; set; }
        public string accountNumber { get; set; }
        public string accountName { get; set; }
        public double mcaAmount { get; set; }
        public double expenseAmount { get; set; }
        public double totalFundingAmount { get; set; }
        public string ownerName { get; set; }
        public string wireTransfer { get; set; }
        public bool contractReviewed { get; set; }
        public bool contractFunded { get; set; }        
        public int bankId { get; set; }
        public int ownerId { get; set; }
        public int documentTypeID { get; set; }
        public int documentID { get; set; }
        public int contractId { get; set; }
        public int merchantId { get; set; }
        public long documentId { get; set; }
        public string fileName { get; set; }
        public int contractStatusId { get; set; }
       
    }    
}