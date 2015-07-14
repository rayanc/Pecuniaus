
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Pecuniaus.Models
{
    public class DeclineModel
    {
        public long merchantId { get; set; }

        [Display(Name = "Decline Reason")]
        public int Declinereason { get; set; }

        [Display(Name = "Decline Notes")]
        public string DeclineNotes { get; set; }
        public int WorkflowId { get; set; }

        public long ContractID { get; set; }

        public IEnumerable<GeneralModel> DeclineReasons { get; set; }

        public int DeclineId { get; set; }
        [Display(Name = "Never Re-evaluate")]
        public bool IsReEvaluvate { get; set; }
        public string Screen { get; set; }

    }
}
