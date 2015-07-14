using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pecuniaus.Renewal.Models
{
    public class DeclineModel
    {
        [Required]        
        public int DeclineId { get; set; }        
        
        [Required]      
        
        [Display(Name = "DeclineNote", ResourceType = typeof(Pecuniaus.Resources.Common))]
        public string DeclineNotes { get; set; }

        [Display(Name = "DeclineReason", ResourceType = typeof(Pecuniaus.Resources.Common))]
        public IEnumerable<GeneralModel> DeclineReasons { get; set; }

        public int WorkflowId { get; set; }
        public long ContractID { get; set; }
    }
}