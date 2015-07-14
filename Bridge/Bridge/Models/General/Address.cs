using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class Address
    {
        public  string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string fax { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string email { get; set; }
        public string country { get; set; }
        public string zipId { get; set; }
        public Int64 addressId { get; set; }
        public Int64 stateId { get; set; }

        public string CountryName { get; set; }
       
    }
}