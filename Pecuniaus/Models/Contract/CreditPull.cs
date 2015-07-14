using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pecuniaus.Models.Contract
{
   public class CreditPull
    {
        [Display(Name = "Credit Pull Type")]
        public IEnumerable<SelectListItem> Type { get; set; }
        
        public int typeid { get; set; }
        
        public CreditPull()
        {
            Type = new List<SelectListItem>();
            
        }

    }
}
