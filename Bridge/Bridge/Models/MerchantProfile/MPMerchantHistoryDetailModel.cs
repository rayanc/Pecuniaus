using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantHistoryDetailModel
    {
        public MPMerchantHistoryDetailModel()
        {
            HistoryDetail = new List<MPMerchantHistoryModel>();
        }
        public DateTime? HistoryStartDate { get; set; }

        public DateTime? HistoryEndDate { get; set; }
        public List<MPMerchantHistoryModel> HistoryDetail { get; set; }
    }
}