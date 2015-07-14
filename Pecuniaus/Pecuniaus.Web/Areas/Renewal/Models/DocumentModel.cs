using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pecuniaus.Renewal.Models
{
    public class DocumentModel
    {
        public long DocumentId { get; set; }
        public long DocumentTypeId { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public string FileDetails { get; set; }
        public long MerchantId { get; set; }
        public long ContractId { get; set; }
        public long UploadUserId { get; set; }
        public long StatusId { get; set; }
       
        
    }
}