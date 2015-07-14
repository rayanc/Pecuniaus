using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPMerchantDocumentModel
    {
        public bool DeleteIt { get; set; }
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadedBy { get; set; }

    }
}