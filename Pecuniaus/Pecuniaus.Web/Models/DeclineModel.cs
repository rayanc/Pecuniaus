using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pecuniaus.Web.Models
{
    public class DeclineModel
    {
        [Required]        
        public int DeclineId { get; set; }        
        
        [Required]      
        [Display(Name = "Decline Notes")]

        public string DeclineNotes { get; set; }

        [Display(Name = "Decline Reason")]
        public IEnumerable<Pecuniaus.Models.GeneralModel> DeclineReasons { get; set; }
     
        [Display(Name = "Decline Reason")]
        public long DeclineReasonId { get; set; }

        public int WorkflowId { get; set; }
        public long ContractID { get; set; }
        public long merchantId { get; set; }
        public string Screen { get; set; }
        public bool IsReEvaluvate { get; set; }
    }
}