using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantAffiliationModel
    {
        public string MerchantName { get; set; }
        public Int64 MerchantId { get; set; }
        [DataType(DataType.Currency)]
        public decimal AECAmount { get; set; }
        [DataType(DataType.Currency)]
        public decimal OwnedAmount { get; set; }
        [DataType(DataType.Currency)]
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