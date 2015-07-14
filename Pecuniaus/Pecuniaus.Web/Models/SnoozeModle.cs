using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pecuniaus.Web.Models
{
    public class SnoozeModel
    {
        public long contractID { get; set; }

        [Required]
        [Display(Name = "Snooze till Date:")]
        public DateTime snoozeDate { get; set; }

        [Required]
        [Display(Name = "Snooze till paid off % age:")]
        public double paidpercent { get; set; }

        public int WorkflowId { get; set; }
    }
}