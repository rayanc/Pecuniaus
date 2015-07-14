using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bridge.Models
{
    public class MPMerchantModel
    {
        public MPMerchantModel()
        {
            merchantStatuses = new List<SelectListItem>();
            Processors = new List<SelectListItem>();
        }
        public int merchantID { get; set; }
        public IEnumerable<SelectListItem> merchantStatuses { get; set; }
        public int merchantStatusID { get; set; }
        public IEnumerable<SelectListItem> Processors { get; set; }
        public int processorID { get; set; }
        public string processorNumber { get; set; }
        public string ownerName { get; set; }
        public string legalName { get; set; }
        public string businessName { get; set; }
        public int ownerID { get; set; }


    }
}