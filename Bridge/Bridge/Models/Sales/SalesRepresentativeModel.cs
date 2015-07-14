using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class SalesRepresentativeModel
    {
        public Int64 salesRepId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string ssn { get; set; }
        public string  jobTitle { get; set; }
        public string fullName { get; set; }
        public long UserId { get; set; }
    }
}