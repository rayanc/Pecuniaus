using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Repository;
using Bridge.Models;

namespace Bridge.BusinessTier
{
    public class NotificationTier : IDisposable
    {
        #region Private Variables
        private INotification notificationRepository;
        #endregion

        #region Contructors
        public NotificationTier() : this(new NotificationRepository()) { }
        public NotificationTier(INotification notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }
        #endregion

        #region Notifications
        public IList<NotificationModel> RetrieveNotifications()
        {
            return notificationRepository.RetrieveNotifications();
        }
        public bool AddNotification(NotificationModel entity)
        {
            try
            {
                notificationRepository.AddNotification(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateNotificationStatus(NotificationModel entity)
        {
            try
            {
                notificationRepository.UpdateNotificationStatus(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Notification Groups
        public IList<NotificationGroupsModel> RetrieveNotificationGroups()
        {
            return notificationRepository.RetrieveNotificationGroups();
        }
        public bool ManageNotificationGroups(NotificationGroupsModel entity)
        {
            try
            {
                notificationRepository.ManageNotificationGroups(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

#endregion

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}