using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pecuniaus.Models.User;

namespace Pecuniaus.Notification.Models
{
    public class NotificationGroupsModel
    {
        public Int64 NotificationGroupID { get; set; }
        public string NotificationGroupName { get; set; }
        public IList<UserModel> NotificationGroupUsers { get; set; }
    }
}