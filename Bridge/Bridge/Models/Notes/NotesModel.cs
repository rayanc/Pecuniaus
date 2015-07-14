using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Models;

namespace Bridge.Models
{
    public class NotesModel
    {
        public Int64 noteTypeId { get; set; }
        public string noteTypeDescription { get; set; }
        public string note { get; set; }
        public string screenName { get; set; }
        public Int64 merchantId { get; set; }
        public Int64 contractId { get; set; }
        public Int64 workFlowId { get; set; }
        public Int64 InsertUserId { get; set; }
        public string UserName { get; set; }
    }
}