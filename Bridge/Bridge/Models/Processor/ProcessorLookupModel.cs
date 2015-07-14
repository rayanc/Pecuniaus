using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class ProcessorLookupModel
    { 
        public int ProcessorId { get; set; }
        public string Name { get; set; } 
        public string ProcessorCode { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }        
    }
}