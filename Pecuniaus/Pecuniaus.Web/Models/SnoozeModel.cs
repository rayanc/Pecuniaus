using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Pecuniaus.Resources.Renewal;

namespace Pecuniaus.Web.Models
{
    public class SnoozeModel
    {
        public Int64 contractID { get; set; }

        //[Required]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        //[Display(Name = "snoozeDate", ResourceType = typeof(Renewal))]
        //[DataType(DataType.Date)]
        //public DateTime snoozeDate { get; set; }

        [Required]
        [Display(Name = "snoozeDate", ResourceType = typeof(Renewal))]
        [DataType(DataType.Date)]
        public DateTime snoozeDate { get; set; }

        [Required]
        [Display(Name = "paidpercentage", ResourceType = typeof(Renewal))]
        public double paidpercent { get; set; }

    }
}