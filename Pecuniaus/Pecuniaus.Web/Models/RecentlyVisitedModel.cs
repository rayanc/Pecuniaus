using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.Web.Models
{
    /// <summary>
    /// Recently visited list of merchants
    /// </summary>
    public class RecentlyVisitedModel 
    {
        public string workflow { get; set; }
        public string merchantName { get; set; }
        public string businessName { get; set; }
        public string legalName { get; set; }
        public string taskName { get; set; }
        public Int64 contractId { get; set; }
        public Int64 workflowId { get; set; }
        public Int64 userId { get; set; }
        public Int64 taskId { get; set; }
        public Int64 merchantId { get; set; }
        public long TaskTypeId { get; set; }
   
        public string RecentlyVisited
        {
            get
            {
                return this.merchantId.ToString() + " - " + (string.IsNullOrEmpty(this.legalName) ? "" : this.legalName);
            }
        }
       

    }
}