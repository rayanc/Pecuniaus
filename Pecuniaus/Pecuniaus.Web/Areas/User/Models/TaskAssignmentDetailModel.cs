using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pecuniaus.Models;
using System.Web.Mvc;

namespace Pecuniaus.User.Models
{
    public class TaskAssignmentDetailModel
    {

        public TaskAssignmentDetailModel()
        {
        }
        public Int64 TaskID { get; set; }
        public Int64 UserID { get; set; }
        public Int64 AssignToUserID { get; set; }
        public string UserName { get; set; }
        public Int64 MerchantID { get; set; }
        public string MerchantName { get; set; }
        public string TaskName { get; set; }
        public string StatusName { get; set; }
        public DateTime AssignedDate { get; set; }
        public IEnumerable<SelectListItem> UserstoAssign { get; set; }
    }
}