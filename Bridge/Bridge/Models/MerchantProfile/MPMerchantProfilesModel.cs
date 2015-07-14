using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantProfilesModel
    {
        public MPMerchantProfilesModel()
        {
        }
        public int ActivityID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int ProcessorId { get; set; }
        public string ProcessorName { get; set; }
        public double Amount { get; set; }
        public int Tickets { get; set; }
        public bool IsAutomated { get; set; }
    }
}