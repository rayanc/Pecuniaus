using System.Collections.Generic;
using System.Web.Mvc;

namespace Pecuniaus.Models
{
    public class KickBack
    {
        public KickBack()
        {
            TaskTypes = new List<SelectListItem>();
        }
        public IEnumerable<SelectListItem> TaskTypes { get; set; }
        public int TaskTypeId { get; set; }
    }
}
