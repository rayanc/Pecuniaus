using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class ProcessorModel
    {
        public int processorId { get; set; }
		public string processorName { get; set; }
        public string companyName { get; set; }
        public string processorRNC { get; set; }
        public string bankAccountNumber { get; set; }
        public string bankAccountName { get; set; }
        public string authorisedOwner { get; set; }
        public Int64 processorTypeId { get; set; }

    }
}