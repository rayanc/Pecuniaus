using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Pecuniaus.Resources.Renewal;


namespace Pecuniaus.Renewal.Models
{
    public class SnoozeModel
    {
        public Int64 contractID { get; set; }
        [Required]
        [Display(Name = "snoozeDate", ResourceType = typeof(Pecuniaus.Resources.Renewal.Renewal))]
        [DataType(DataType.Date)]
        public DateTime snoozeDate { get; set; }

        [Required]
        [Display(Name = "paidpercentage", ResourceType = typeof(Pecuniaus.Resources.Renewal.Renewal))]
        public double snoozePercent { get; set; }

    }
}