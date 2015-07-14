using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.Web.Models
{
    public class NotesModel
    {
        public Int64 noteId { get; set; }
        public Int64 noteTypeId { get; set; }
        public string note { get; set; }
        public string insertDate { get; set; }
        public string screenName { get; set; }
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
        public Int64 workFlowId { get; set; }
    }
}