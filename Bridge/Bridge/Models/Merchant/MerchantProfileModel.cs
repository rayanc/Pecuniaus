using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models.Merchant
{
    /// <summary>
    /// Card profiles for the merchant
    /// </summary>
    public class MerchantProfileModel
    {
        public Int64 merchantId { get; set; }
        public DateTime processedDate { get; set; }
        public DateTime activityStartDate { get; set; }
        public DateTime activityEndDate { get; set; }
        public double totalAmount { get; set; }
        public Int64 tickets { get; set; }
    }
}