using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.Collection.Models
{
    public class CreditVolumesModel
    {
        public CreditVolumesModel()
        {
            ProcessorTypes = new List<SelectListItem>();
            ProcessorByMerchants = new List<SelectListItem>();
            ListYears = new List<SelectListItem>();
            ListMonths = new List<SelectListItem>();
        }

        [Display(Name = "ProcessorName", ResourceType = typeof(Resources.CreditVolumes.CreditVolumes))]
        public IEnumerable<SelectListItem> ProcessorTypes { get; set; }
        [Required]
        [Display(Name = "Processor Name")]
        public int processorTypeId { get; set; }

        [Display(Name = "Processor Id")]
        public IEnumerable<SelectListItem> ProcessorByMerchants { get; set; }
        [Required]
        [Display(Name = "Processor ID")]
        public int processorId { get; set; }

        [Display(Name = "Year")]
        public IEnumerable<SelectListItem> ListYears { get; set; }
        [Required]
        [Display(Name = "Year", ResourceType = typeof(Resources.CreditVolumes.CreditVolumes))]
        public int year { get; set; }

        [Display(Name = "Month")]
        public IEnumerable<SelectListItem> ListMonths { get; set; }
        [Required]
        [Display(Name = "Month", ResourceType = typeof(Resources.CreditVolumes.CreditVolumes))]
        public int month { get; set; }

        [Required]
        //[RegularExpression("^(\\+)?\\d+(\\.\\d+)?$", ErrorMessage = "Amount is invalid")]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        public decimal totalAmount { get; set; }

        [Required]
        [RegularExpression("^\\d+$", ErrorMessage = "Total Tickets is invalid")]
        [Display(Name = "Total Ticket")]
        public Int64 totalTickets { get; set; }

        public Int64 creditcardActivityId { get; set; }
        public string processorNumber { get; set; }
        
        
        public int isActive { get; set; }
        public string monthname { get; set; }

        [Display(Name = "Processor")]
        public string ProcessorName { get; set; }

        public string processorRNC { get; set; }
        

        
        


        

    }
}