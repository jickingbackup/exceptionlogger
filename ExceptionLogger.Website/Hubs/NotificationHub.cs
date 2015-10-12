using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ExceptionLogger.Website.Hubs
{
    public class NotificationHub : Hub
    {
        public void NotifyClients()
        {
            Clients.All.notifyClients();
        }

        public void NotifyAllClients(string title,string message,string url)
        {
            Clients.All.notifyAllClients(title,message,url);
        }
    }
}