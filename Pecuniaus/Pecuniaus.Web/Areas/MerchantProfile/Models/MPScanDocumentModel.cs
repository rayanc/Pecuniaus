using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPScanDocumentModel
    {
        public IEnumerable<SelectListItem> DocumentTypes { get; set; }
        [Display(Name = "DocumentType", ResourceType = typeof(Resources.Contract.DocumentScan))]
        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DocumentScan),
                      ErrorMessageResourceName = "DocumentTypeRequired")]
        public int DocumentTypeID { get; set; }

        public int DocumentID { get; set; }
    }
}