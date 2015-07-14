using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MerchantProcessorModel
    {
        public int processorId { get; set; }
        public int processorTypeId { get; set; }

        public string processorNumber { get; set; }
        public string processorName { get; set; }

        public DateTime firstprocessedDate { get; set; }
    }
}