using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR;

namespace LiveBidsWithSignalR
{
    public class LiveBidConnection : PersistentConnection
    {
        protected override System.Threading.Tasks.Task OnReceivedAsync(string connectionId, string eventType)
        {
            return Connection.Broadcast(eventType);
        }
    }
}