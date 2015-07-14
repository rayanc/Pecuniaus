using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class DocumentsModel
    {
        public Int64 documentId { get; set; }
        public Int64 documentTypeId { get; set; }
        public string documentName { get; set; }
        public string fileName { get; set; }
        public string fileDetails { get; set; }
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
        public Int64 uploadUserId { get; set; }
        public string uploadedBy { get; set; }
        public DateTime uploadedDate { get; set; }
        public Int64 StatusId { get; set; }
    }
}