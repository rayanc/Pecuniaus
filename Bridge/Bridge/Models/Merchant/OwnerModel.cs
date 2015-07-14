using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class OwnerModel
    {
        public string ownerFirstName { get; set; }
        public string ownerLastName { get; set; }
        public Int64 ownerId { get; set; }
        public Int64 merchantId { get; set; }
        public string phone1 { get; set; }
        public string CellNumber { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string email { get; set; }
        public string ssn { get; set; }
        public DateTime ownerDOB { get; set; }
        public string state { get; set; }
        public string zipId { get; set; }
        public string cityId { get; set; }
        public string stateId { get; set; }
        public string zip { get; set; }
        public string phone2 { get; set; }
        public string fax { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public decimal rentamount { get; set; }
        public string PassportNumber { get; set; }
        public Int64 addressId { get; set; }
        public Int64 contactId { get; set; }

        public bool Authorized { get; set; }

    }
}