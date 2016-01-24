using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Swappler.Hubs
{
    public class SwapRequestHub : Hub
    {
        public override Task OnConnected()
        {
            var username = Context.RequestCookies["signalr-username"].Value;
            var connectionId = Context.ConnectionId;

            //UsernameConnectionMap.AddMapping(username, connectionId);

            return base.OnConnected();
        }
    }
}