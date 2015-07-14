using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantContractSalesRepresentativeModel
    {
        public Int64 ContractId { get; set; }
        public int SalesRepId { get; set; }
        public string SalesRepName { get; set; }
        public string SaleType { get; set; }
        public int AgreementType { get; set; }
        public double Commission { get; set; }
        public double RenewalCommission { get; set; }
        public bool IsPrimary { get; set; }
        public Int64 UserId { get; set; }
    }
}