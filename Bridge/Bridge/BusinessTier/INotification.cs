using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bridge.Models;

namespace Bridge.BusinessTier
{
    public interface INotification
    {
        IList<NotificationModel> RetrieveNotifications();
        bool AddNotification(NotificationModel entity);
        bool UpdateNotificationStatus(NotificationModel entity);
        IList<NotificationGroupsModel> RetrieveNotificationGroups();
        bool ManageNotificationGroups(NotificationGroupsModel entity);
    }
}
