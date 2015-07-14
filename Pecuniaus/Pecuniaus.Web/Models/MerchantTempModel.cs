using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.Web.Models
{
    public class MerchantTempModel
    {
        // public Int64 merchantId { get; set; }
        public string merchantName { get; set; }
        public string businessName { get; set; }
        public string legalName { get; set; }
        public string ownerName { get; set; }
        public string rnc { get; set; }
        public Int16 isSynced { get; set; }
        public DateTime businessStartDate { get; set; }
        public Int64 businessTypeId { get; set; }
        public int propertyType { get; set; }
        public int industryTypeid { get; set; }
        public int salesRepId { get; set; }
        public string assignedSalesRep { get; set; }
        public string cellPhone { get; set; }
        public string homePhone { get; set; }
        public bool IsDuplicate { get; set; }
        public string taskName { get; set; }
        public string taskStatus { get; set; }
        public long userId { get; set; }
    }
}