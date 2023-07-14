using eStat.BLL.Core;
using eStat.BLL.Jobs.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace eStat.BLL.Jobs
{
    public class NotificationSystemHub: Hub, INotificationSystemHub
    {
        public readonly static ConnectionMapping _connections =
            new ConnectionMapping();
        protected IHubContext<NotificationSystemHub> _context;

        public NotificationSystemHub(IHubContext<NotificationSystemHub> context)
        {
            _context = context;
        }

        public async Task NotifyUser(string userId, string message)
        {
            Console.WriteLine(userId);
            foreach (var connectionId in _connections.GetConnections(userId.ToLowerInvariant()))
            {
                Console.WriteLine(connectionId);
                await this._context.Clients.Client(connectionId).SendAsync("Notify", message);
            }
            //await Clients.User(userId).SendAsync(message);
        }

        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var key = httpContext.Request.Query["key"];
            Console.WriteLine($"{key}");
            Console.WriteLine($"{Context.ConnectionId}");
            _connections.Add(key.ToString(), Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public void OnDisconnected(string userId, string connectionId)
        {
            _connections.Remove(userId, connectionId);
        }
    }
}
