using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantCollectionModel
    {
        public Int64 ContractId { get; set; }
        public string ContractStatus { get; set; }
        public Int64 CollectionId { get; set; }
        public string CollectionType { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime DateRemoved { get; set; }
        public decimal AECAmount { get; set; }
        public decimal AmountWhenRemovedFromCollection { get; set; }
        public int AffiliationId { get; set; }
        public decimal OwnedAmount { get; set; }
        public int RetentionTime { get; set; }
        public DateTime FundingDate { get; set; }
        public string MerchantStatus { get; set; }

    }
}