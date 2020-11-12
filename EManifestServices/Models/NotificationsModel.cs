using EManifestServices.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EManifestServices.Models
{
    public class NotificationsModel
    {
        public NotificationsModel()
        {
            Notifications = new List<NotificationModel>();
        }
        public List<NotificationModel> Notifications { get; set; }
        public int NewCount { get; set; }
    }

    public class NotificationModel
    {
        public Guid NotificationId { get; set; }
        public string Header { get; set; }
        public string Details { get; set; }
        public bool Status { get; set; }
        public bool? Read { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? NotificationDate { get; set; }
    }
}