using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.Contract.Models
{
    public class SearchResultModel
    {
        public long MerchantId { get; set; }
        public string MerchantName { get; set; }
        public string TaskName { get; set; }
        public long TaskTypeId { get; set; }
        public string TaskStatus { get; set; }
        public long TaskStatusId { get; set; }
        public string Rnc { get; set; }
        public string AssignedSalesRep { get; set; }
    
    }
}