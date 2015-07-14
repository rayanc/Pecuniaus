using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pecuniaus.Models.Prequel
{
    public class Email
    {
        [Display(Name = "Email", ResourceType = typeof(Resources.Common))]
        [Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        public string[] SelectedEmail { get; set; }

        [Display(Name = "EmailRecipient", ResourceType = typeof(Resources.Common))]
        public IEnumerable<SelectListItem> EmailList { get; set; }

    }
}
