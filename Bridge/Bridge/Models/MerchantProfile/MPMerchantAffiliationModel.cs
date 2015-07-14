using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantAffiliationModel
    {
        public string MerchantName { get; set; }
        public Int64 MerchantId { get; set; }
        public decimal AECAmount { get; set; }
        public decimal OwnedAmount { get; set; }
        public decimal PendingAmount { get; set; }
        public int RetentionTime { get; set; }
        public DateTime FundingDate { get; set; }
        public string MerchantStatus { get; set; }
        public DateTime CreationDate { get; set; }
        public dynamic LastEvaluationDate { get; set; }
        public dynamic RequestedCCVolumes { get; set; }
        public string VolumeStatus { get; set; }
    }
}