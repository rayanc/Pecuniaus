using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.MerchantProfile.Models
{
    public class MPDocumentModel
    {
        public long DocumentId { get; set; }
        public long DocumentTypeId { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public string FileDetails { get; set; }
        public long MerchantId { get; set; }
        public long ContractId { get; set; }
        public long UploadUserId { get; set; }
    }
}