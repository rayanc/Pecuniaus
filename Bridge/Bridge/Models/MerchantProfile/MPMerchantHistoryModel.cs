using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantHistoryModel
    {
        public DateTime HistoryDate { get; set; }

        public string Field { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }
    }
}