using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Live.Hubs;
using Live.Models;




namespace Live.Dispatchers
{
    public class ChatHubDispatcher : IHubDispatcher
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatHubDispatcher(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task Dispatch(Notification notification) =>
            _hubContext
                .Clients
                .All
                .SendAsync(nameof(Notification), notification);
    }
}