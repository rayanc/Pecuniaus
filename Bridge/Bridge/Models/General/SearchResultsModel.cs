using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models.General
{
    public class SearchResultsModel
    {
        public Int64 merchantId { get; set; }
        public Int64 contractid { get; set; }
        public string merchantName { get; set; }
        public string taskName { get; set; }
        public Int64 taskTypeId { get; set; }
        public string tasKStatus { get; set; }
        public Int64 taskStatusId { get; set; }
        public string rnc { get; set; }
        public string assignedSalesRep { get; set; }
        public string businessName { get; set; }
        public string legalName { get; set; }
        public string ownerName { get; set; }
        public string RentFlag { get; set; }
        public DateTime StatusDate { get; set; }
        public string assignedUser { get; set; }
        public string industrytype { get; set; }

        public DateTime? CompletionDate { get; set; }

        public DateTime? AssignedDate { get; set; }

        public string merchantStatus { get; set; }

    }
}
