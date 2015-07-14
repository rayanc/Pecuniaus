using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class MPMerchantProfileDetailModel
    {
         public MPMerchantProfileDetailModel()
        {
            ProfileDetail = new List<MPMerchantProfilesModel>();
        }

         public Int64 MerchantID { get; set; }
         public int ProcessorId { get; set; }
         public string ProcessorNumber { get; set; }
         public double GrossYearlySale { get; set; }
         public double AverageMonthlyTicket { get; set; }
         public double MonthlyCCSale { get; set; }
         public double AverageMonthlyCCValue { get; set; }
         public List<MPMerchantProfilesModel> ProfileDetail { get; set; }
    }
}