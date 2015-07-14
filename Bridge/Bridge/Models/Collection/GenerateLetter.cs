using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models 
{
    public class GenerateLetter
    {
        public DateTime currentDate { get; set; }
        public string legalName { get; set; }
        public string ownerName { get; set; }
        public string address { get; set; }
        public string province { get; set; }
        public string contractNumber { get; set; }
        public string merchantName { get; set; }
        public DateTime dateLastActivity { get; set; }
        public double pendingAmount { get; set; }
        public string toEmail { get; set; }
    }   
   
}