using System;
using System.Collections.Generic;

namespace Pecuniaus.Models
{
    public class UserDashboardModel
    {
        private IList<AssignedTask> _userAssignedTasks;
        private IList<CollectionActivity> _collectionActivities;
        private IList<TotalSale> _totalSales;
        private IList<NewLead> _newLeads;

        public UserDashboardModel()
        {
            _userAssignedTasks = new List<AssignedTask>();
            _collectionActivities = new List<CollectionActivity>();
            _totalSales = new List<TotalSale>();
            _newLeads = new List<NewLead>();

        }
        public IList<AssignedTask> UserAssignedTasks { get { return _userAssignedTasks ?? new List<AssignedTask>(); } set { _userAssignedTasks = value; } }
        public IList<CollectionActivity> CollectionActivities { get { return _collectionActivities ?? new List<CollectionActivity>(); } set { _collectionActivities = value; } }
        public IList<TotalSale> TotalSales { get { return _totalSales?? new List<TotalSale> (); } set { _totalSales = value; } }
        public IList<NewLead> NewLeads { get { return _newLeads ?? new List<NewLead>(); } set { _newLeads = value; } }
    }

    public class AssignedTask
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
