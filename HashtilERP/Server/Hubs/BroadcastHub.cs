using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using HashtilERP.Shared.Models;

namespace HashtilERP.Server.Hubs
{
    public class BroadcastHub : Hub
    {
      
        public async Task SendMessage()
        {            
            await Clients.All.SendAsync("ReceiveMessage");
        }

        public async Task SendMessageForPickPassport()
        {
            await Clients.All.SendAsync("PickPassport");
        }
    }
}
