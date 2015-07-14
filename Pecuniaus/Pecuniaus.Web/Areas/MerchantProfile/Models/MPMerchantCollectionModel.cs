using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantCollectionModel
    {
        public Int64 ContractId { get; set; }
        public string ContractStatus { get; set; }
        public Int64 CollectionId { get; set; }
        public string CollectionType { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime DateRemoved { get; set; }
        [DataType(DataType.Currency)]
        public decimal AECAmount { get; set; }
        [DataType(DataType.Currency)]
        public decimal AmountWhenRemovedFromCollection { get; set; }
        public int AffiliationId { get; set; }
        [DataType(DataType.Currency)]
        public decimal OwnedAmount { get; set; }
        public int RetentionTime { get; set; }
        public DateTime FundingDate { get; set; }
        public string MerchantStatus { get; set; }
    }
}