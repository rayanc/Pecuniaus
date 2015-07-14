
using System;
namespace Pecuniaus.Models
{
    public class SearchResultModel
    {
        public long MerchantId { get; set; }
        public long contractid { get; set; }
        public string MerchantName { get; set; }
        public string TaskName { get; set; }
        public long TaskTypeId { get; set; }
        public string TaskStatus { get; set; }
        public long TaskStatusId { get; set; }
        public string Rnc { get; set; }
        public string AssignedSalesRep { get; set; }
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
