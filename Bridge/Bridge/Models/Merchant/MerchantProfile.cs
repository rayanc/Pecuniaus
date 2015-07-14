using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    /// <summary>
    /// Card profiles for the merchant
    /// </summary>
    public class MerchantProfile : ProcessorModel
    {
        public Int64 merchantId { get; set; }
        public Int64 totalTickets { get; set; }
        public int retrievalSource { get; set; }
        public double totalAmount { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime processedDate { get; set; }
        public Int64 creditcardActivityId { get; set; }
        public int year { get; set; }
        public int isprocessed { get; set; }
        public int month { get; set; }
        public Int64 contractId { get; set; }
        public Int64 isActive { get; set; }

    }		 
}