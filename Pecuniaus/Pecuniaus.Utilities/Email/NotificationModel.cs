using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pecuniaus.Utilities.Email
{
    public class NotificationModel
    {
        public Int64 NotificationQueueId { get; set; }
        public string NotificationFileName { get; set; }
        public string NotificationStatus { get; set; }
    }
}