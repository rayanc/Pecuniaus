using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pecuniaus.Models.Contract
{
   public class ScanDocumentModel
    {
        public IEnumerable<SelectListItem> DocumentTypes { get; set; }
        [Display(Name = "DocumentType", ResourceType = typeof(Resources.Contract.DocumentScan))]
        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DocumentScan),
                      ErrorMessageResourceName = "DocumentTypeRequired")]
        public int DocumentTypeID { get; set; }

        public int DocumentID { get; set; }

        [Display(Name = "IsPending", ResourceType = typeof(Resources.Contract.DocumentScan))]
        public bool IsPending { get; set; }
    }
}
