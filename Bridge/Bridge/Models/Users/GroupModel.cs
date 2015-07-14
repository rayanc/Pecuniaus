using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class GroupModel:BaseModelUser
    {
        public Int32 GroupID { get; set; }
        public string GroupName { get; set; }  
        public Int32? GroupTypeID { get; set; }
        public string GroupTypeName { get; set; } 

    }
}