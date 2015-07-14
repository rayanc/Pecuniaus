using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bridge.Models
{
    public class NotificationGroupsModel
    {
        public Int64 NotificationGroupID { get; set; }
        public string NotificationGroupName { get; set; }
        public IList<UserModel> NotificationGroupUsers { get; set; }
    }
}