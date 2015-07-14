using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class GeneralModel
    {
        public Int64 keyId { get; set; }
        public string description { get; set; }
    }
    public class SnoozeModel
    {

        public Int64 contractId { get; set; }
        public double snoozePercent { get; set; }
       public DateTime snoozeDate { get; set; }


    }
}