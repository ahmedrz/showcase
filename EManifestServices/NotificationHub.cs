using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EManifestServices.DAL;
using EManifestServices.Models;
using Microsoft.AspNet.SignalR;

namespace EManifestServices
{
    public class NotificationHub : Hub
    {
        public static ConcurrentDictionary<string, MyUserType> MyUsers = new ConcurrentDictionary<string, MyUserType>();
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }
        public static void AddNotification(Guid userId, Notifications notification)
        {
            var mappedNotification = AutoMapper.Mapper.Map<NotificationModel>(notification);
            var clients = MyUsers.Where(u => u.Value.DbUserId == userId).Select(u => u.Key).ToList();
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            hubContext.Clients.Clients(clients).addNewNotification(notification);
            //hubContext.Clients.All.addNewNotification(mappedNotification);
        }

        public override Task OnConnected()
        {
            var qsUserid = Context.QueryString["id"];
            Guid UserId = Guid.Empty;
            if (qsUserid != null && qsUserid != "undefined" && Guid.TryParse(qsUserid, out UserId))
            {
                MyUsers.AddOrUpdate(Context.ConnectionId, new MyUserType() { DbUserId = UserId }, (key, value) => { return new MyUserType() { DbUserId = UserId }; });
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            MyUserType garbage;

            MyUsers.TryRemove(Context.ConnectionId, out garbage);

            return base.OnDisconnected(stopCalled);
        }
        public override Task OnReconnected()
        {
            var qsUserid = Context.QueryString["id"];
            Guid UserId = Guid.Empty;
            if (qsUserid != null && qsUserid != "undefined" && Guid.TryParse(qsUserid, out UserId))
            {
                MyUsers.AddOrUpdate(Context.ConnectionId, new MyUserType() { DbUserId = UserId }, (key, value) => { return new MyUserType() { DbUserId = UserId }; });
            }
            return base.OnReconnected();
        }

    }
    public class MyUserType
    {
        public Guid DbUserId { get; set; }
        // Can have whatever you want here
    }

}