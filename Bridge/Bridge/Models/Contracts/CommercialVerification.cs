using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class CommercialVerification
    {
        public string businessName { get; set; }
        public string telephone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public Int64 StateId { get; set; }
        public string fileName { get; set; }
        public string fileDetails { get; set; }
        public Int64 addressId { get; set; }
    }
}