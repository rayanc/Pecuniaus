using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models.Merchant
{
    public class MerchantTradeReference
    {
        public long ReferenceId { get; set; }      
        public string ReferenceName { get; set; }
        public string PhoneNumber { get; set; }
        public long MerchantId { get; set; }
        public long ContractId { get; set; }
    }
}