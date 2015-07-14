using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantProcessorInfoModel
    {
        public MPMerchantProcessorInfoModel()
        {
        }
        //public IEnumerable<SelectListItem> ProcessorCompar { get; set; }

        [Display(Name = "ProcessorTerminals", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public Int64 Terminal { get; set; }

        public Int64 processorId { get; set; }

        public Int64 processorTypeId { get; set; }

        [Display(Name = "ProcessorName", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string ProcessorName { get; set; }

        [Display(Name = "ProcessorNumber", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string ProcessorNumber { get; set; }

        [Display(Name = "ProcessorPhoneNumber", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string ProcessorPhoneNumber { get; set; }

        [Display(Name = "ProcessorIndustryType", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        public string IndustryType { get; set; }
        public int IndustryTypeID { get; set; }

        [Display(Name = "ProcessorFirstProcessedDate", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        [DataType(DataType.Date)]
        public DateTime? FirstProcessedDate { get; set; }

        [Display(Name = "EditProcessorGracePeriod", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Business))]
        [DataType(DataType.Date)]
        public DateTime? DateGracePeriod { get; set; }
        public IEnumerable<SelectListItem> IndustryTypes { get; set; }
        public IEnumerable<SelectListItem> Processors { get; set; }
    }
}