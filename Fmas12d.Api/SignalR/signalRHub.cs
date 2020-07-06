using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Fmas12d.Api.SignalR
{
    public class signalRHub : Hub
    {
        public async Task SendNotification(string user, string message) {
          await Clients.All.SendAsync("ReceiveNotification", user, message);
        }
    }
}