using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantProfilesModel
    {
        public MPMerchantProfilesModel()
        {
            MonthsList = new List<SelectListItem>() { 
                new SelectListItem(){Value="1",Text="January"},
                new SelectListItem(){Value="2",Text="February"},
                new SelectListItem(){Value="3",Text="March"},
                new SelectListItem(){Value="4",Text="April"},
                new SelectListItem(){Value="5",Text="May"},
                new SelectListItem(){Value="6",Text="June"},
                new SelectListItem(){Value="7",Text="July"},
                new SelectListItem(){Value="8",Text="August"},
                new SelectListItem(){Value="9",Text="September"},
                new SelectListItem(){Value="10",Text="October"},
                new SelectListItem(){Value="11",Text="November"},
                new SelectListItem(){Value="12",Text="December"}
            };

            Processors = new List<SelectListItem>();
        }
        public int ActivityID { get; set; }

        //[Display(Name = "Month", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Profiles))]
        public int Month { get; set; }

        [Display(Name = "Month", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Profiles))]
        public string MonthName { get { return MonthsList.Where(m => m.Value == Month.ToString()).FirstOrDefault().Text; } }

        [Display(Name = "Year", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Profiles))]
        public int Year { get; set; }

        public int ProcessorId { get; set; }

        [Display(Name = "ProcessorName", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Profiles))]
        public string ProcessorName { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Profiles))]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Display(Name = "Tickets", ResourceType = typeof(Pecuniaus.Resources.MerchantProfile.Profiles))]
        public int Tickets { get; set; }

        public bool IsAutomated { get; set; }

        public IEnumerable<SelectListItem> MonthsList { get; set; }
        public IEnumerable<SelectListItem> Processors { get; set; }
    }
}