using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class DeclineModel
    {
        public long merchantId { get; set; }
       public int declinereason { get; set; }
        public string declineNotes { get; set; }
        public int workflowId { get; set; }
        public int declineId { get; set; }
        public bool IsReEvaluvate { get; set; }
        public string Screen { get; set; }

    }
}