using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pecuniaus.Web.Models
{
    public class MerchantDocumentModel
    {
        [Display(Name = "DocumentId", ResourceType = typeof(Resources.Collection.Document))]
        public long DocumentId { get; set; }
        [Display(Name = "DocumentType", ResourceType = typeof(Resources.Collection.Document))]
        public string DocumentType { get; set; }
        public long DocumentTypeId { get; set; }
        [Display(Name = "DocumentName", ResourceType = typeof(Resources.Collection.Document))]
        public string DocumentName { get; set; }
        [Display(Name = "FileName", ResourceType = typeof(Resources.Collection.Document))]
        public string FileName { get; set; }
        [Display(Name = "FileDetails", ResourceType = typeof(Resources.Collection.Document))]
        public string FileDetails { get; set; }
        
        public long MerchantId { get; set; }          
        public long ContractId { get; set; }

        [Display(Name = "UploadedBy", ResourceType = typeof(Resources.Collection.Document))]
        public string UploadedBy { get; set; }
        public long UploadUserId { get; set; }
        [Display(Name = "UploadedDate", ResourceType = typeof(Resources.Collection.Document))]
        public DateTime UploadedDate { get; set; }
    }
}