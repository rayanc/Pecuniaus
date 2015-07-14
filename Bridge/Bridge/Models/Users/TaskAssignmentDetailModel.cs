using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class TaskAssignmentDetailModel
    {
        public Int64 TaskID { get; set; }
        public Int64 UserID { get; set; }
        public string UserName { get; set; }
        public Int64 MerchantID { get; set; }
        public string MerchantName { get; set; }
        public string TaskName { get; set; }
        public string StatusName { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}