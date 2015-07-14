using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantModel
    {
        public MPMerchantModel()
        {
            merchantStatuses = new List<SelectListItem>();
            Processors = new List<SelectListItem>();
        }
        [Display(Name = "MerchantID", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Search))]
        public Int64? merchantID { get; set; }
        public IEnumerable<SelectListItem> merchantStatuses { get; set; }
        [Display(Name = "MerchantStatus", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Search))]
        public int merchantStatusID { get; set; }
        public IEnumerable<SelectListItem> Processors { get; set; }
        [Display(Name = "Processor", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Search))]
        public int processorID { get; set; }
        [Display(Name = "ProcessorNumber", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Search))]
        public string processorNumber { get; set; }
        [Display(Name = "OwnerName", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Search))]
        public string ownerName { get; set; }
        [Display(Name = "LegalName", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Search))]
        public string legalName { get; set; }
        [Display(Name = "BusinessName", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Search))]
        public string businessName { get; set; }
        [Display(Name = "OwnerID", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Search))]
        public int? ownerID { get; set; }
    }
}