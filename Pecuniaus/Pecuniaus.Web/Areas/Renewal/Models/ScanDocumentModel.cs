using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pecuniaus.Renewal.Models
{
    public class ScanDocumentModel
    {
        public IEnumerable<SelectListItem> DocumentTypes { get; set; }

        [Display(Name = "DocumentType", ResourceType = typeof(Resources.Renewal.Renewal))]
        //[Required(ErrorMessageResourceType = typeof(Resources.Renewal.Renewal),
        //              ErrorMessageResourceName = "DocumentTypeRequired")]
        public int DocumentTypeID { get; set; }

        public int DocumentID { get; set; }

        public bool IsPending { get; set; }
    }
}