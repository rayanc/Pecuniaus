using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pecuniaus.Web.Models
{
    public class MerchantScanDocumentModel
    {        public IEnumerable<SelectListItem> DocumentTypes { get; set; }
        [Display(Name = "DocumentType", ResourceType = typeof(Resources.Contract.DocumentScan))]
        [Required(ErrorMessageResourceType = typeof(Resources.Contract.DocumentScan),
                      ErrorMessageResourceName = "DocumentTypeRequired")]
        public long DocumentTypeID { get; set; }

        public long DocumentID { get; set; }

        public string FileName { get; set; }

        public string FileDetails { get; set; }
    }
}