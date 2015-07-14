using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Bridge.Models
{
    public class MPMerchantDocumentsDetailModel
    {
        public MPMerchantDocumentsDetailModel()
        {
            Documents = new List<MPMerchantDocumentModel>();
            DocumentTypes = new List<SelectListItem>();

        }
        public IEnumerable<MPMerchantDocumentModel> Documents { get; set; }
        public List<SelectListItem> DocumentTypes { get; set; }
    }
}