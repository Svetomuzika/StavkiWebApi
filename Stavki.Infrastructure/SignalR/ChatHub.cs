using Microsoft.AspNetCore.SignalR;
using Stavki.Data.Data;

namespace Stavki.Infrastructure.SignalR
{
    public class ChatHub : Hub
    {
        public async Task Send(string comment)
        {
            await Clients.All.SendAsync("Receive", comment);
        }
    }
}
