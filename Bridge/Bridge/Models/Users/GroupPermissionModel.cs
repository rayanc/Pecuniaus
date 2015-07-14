using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class GroupPermissionModel : BaseModelUser
    {
        public int GroupRightID { get; set; }
        public int GroupID { get; set; }
        public int ModuleID { get; set; }
        public int PageId { get; set; }
        public string ModuleName { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        public bool Edit { get; set; }
    }
}