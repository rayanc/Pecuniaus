using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantDocumentsDetailModel
    {
        public MPMerchantDocumentsDetailModel()
        {
            Documents = new List<MPMerchantDocumentModel>();
            DocumentTypes = new List<SelectListItem>();

        }
        public int DocumentTypeId { get; set; }
        public IEnumerable<MPMerchantDocumentModel> Documents { get; set; }
        public IEnumerable<SelectListItem> DocumentTypes { get; set; }
    }
}