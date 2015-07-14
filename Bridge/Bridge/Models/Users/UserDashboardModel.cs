using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models.Users
{
    public class UserDashboardModel
    {
        public List<AssignedTasks> UserAssignedTasks { get; set; }
        public List<CollectionActivity> CollectionActivities { get; set; }
        public List<TotalSale> TotalSales { get; set; }
        public List<NewLead> NewLeads { get; set; }
       
    }


    public class AssignedTasks
    {

        public string WorkFlowName { get; set; }

        public string TaskName { get; set; }

        public string LegalName { get; set; }

        public DateTime AssignedDate { get; set; }

        public string StatusName { get; set; }

        public Int64 TaskTypeId { get; set; }

        public Int64 MerchantId { get; set; }
    }

    public class CollectionActivity
    {
        public string StatusName { get; set; }
        public string LegalName { get; set; }
        public DateTime AssignedDate { get; set; }
    }

    public class TotalSale
    {
        public string Description { get; set; }
        public double LoanedAmount { get; set; }
        public double PaidAmount { get; set; }
        public double BalanceAmount { get; set; }

        public DateTime fundingDate { get; set; }
    }

    public class NewLead
    {
        public string LeadSource { get; set; }
        public string TaskName { get; set; }
        public string LegalName { get; set; }
        public DateTime AssignedDate { get; set; }
        public string WorkFlowName { get; set; }
    }
}