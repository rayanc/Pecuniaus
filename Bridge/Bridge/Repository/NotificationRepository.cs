using Bridge.BusinessTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bridge.Models;

namespace Bridge.Repository
{
    public class NotificationRepository : IDisposable, INotification
    {

        #region Notification
        public IList<NotificationModel> RetrieveNotifications()
        {
            return new DataAccess.DataAccess().ExecuteReader<NotificationModel>("avz_NOT_spretrieveNotifications");
        }
        public bool AddNotification(NotificationModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("AVZ_NOT_spAddNotification", new
            {
                NotificationFileName = entity.NotificationFileName
            });
        }
        public bool UpdateNotificationStatus(NotificationModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("AVZ_NOT_spUpdateNotificationStatus", new
            {
                NotificationQueueId = entity.NotificationQueueId,
                NotificationStatus = entity.NotificationStatus
            });
        }
        #endregion

        #region Notification Groups
        public IList<NotificationGroupsModel> RetrieveNotificationGroups()
        {
            return new DataAccess.DataAccess().ExecuteReader<NotificationGroupsModel>("avz_NOTGRP_spretrieveNotificationGroups");
        }
        public bool ManageNotificationGroups(NotificationGroupsModel entity)
        {
            return new DataAccess.DataAccess().ExecuteNonQuery("AVZ_NOT_spAddNotification", new
            {
                NotificationFileName = entity.NotificationGroupName
            });
        }
        #endregion

        public void Dispose()
        {
            // throw new NotImplementedException();
        }
    }
}