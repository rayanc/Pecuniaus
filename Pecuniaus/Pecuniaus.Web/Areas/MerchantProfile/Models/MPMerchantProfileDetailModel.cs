using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantProfileDetailModel
    {
        public MPMerchantProfileDetailModel()
        {
            ProfileDetail = new List<MPMerchantProfilesModel>();
            Processors = new List<SelectListItem>();
        }

        public Int64 MerchantID { get; set; }
        
        [Display(Name = "Processor", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Profiles))]
        public int ProcessorId { get; set; }
        [Display(Name = "ProcessorNumber", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Profiles))]
        public string ProcessorNumber { get; set; }
        [Display(Name = "GrossYearlySales", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Profiles))]
        [DataType(DataType.Currency)]
        public decimal GrossYearlySale { get; set; }
        [Display(Name = "AverageMonthlyTicket", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Profiles))]
        public double AverageMonthlyTicket { get; set; }
        [Display(Name = "MonthlyCreditCardSales", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Profiles))]
        [DataType(DataType.Currency)]
        public decimal MonthlyCCSale { get; set; }

        [Display(Name = "AverageMonthlyCCValue", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Profiles))]
        [DataType(DataType.Currency)]
        public decimal AverageMonthlyCCValue { get; set; }

        public List<MPMerchantProfilesModel> ProfileDetail { get; set; }
        public IEnumerable<SelectListItem> Processors { get; set; }
    }
}