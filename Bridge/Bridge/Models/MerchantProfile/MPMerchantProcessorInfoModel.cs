using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bridge.Models
{
    public class MPMerchantProcessorInfoModel
    {
        public MPMerchantProcessorInfoModel()
        {
        }

        public Int64 Terminal { get; set; }
        public Int64 processorId { get; set; }
        public Int64 processorTypeId { get; set; }
        public string ProcessorName { get; set; }
        public string ProcessorNumber { get; set; }
        public string ProcessorPhoneNumber { get; set; }
        public int IndustryTypeID { get; set; }
        public string IndustryType { get; set; }
        public DateTime FirstProcessedDate { get; set; }
        public DateTime DateGracePeriod { get; set; }
    }
}