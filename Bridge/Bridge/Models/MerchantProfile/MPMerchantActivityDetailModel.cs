using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantActivityDetailModel
    {
        public MPMerchantActivityDetailModel()
        {
            ActivityDetail = new List<MPMerchantActivityModel>();
        }

         public Int64 MerchantID { get; set; }
         public int ProcessorTypeId { get; set; }
         public string ProcessorNumber { get; set; }
         public DateTime? ActivityFrom { get; set; }
         public DateTime? ActivityTo { get; set; }
         public List<MPMerchantActivityModel> ActivityDetail { get; set; }
    }
}