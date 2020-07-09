using Microsoft.AspNetCore.SignalR;

namespace Fmas12d.Api.SignalR
{
    public class SignalRMessenger : ISignalRMessenger
    {
        private readonly IHubContext<SignalRHub> _hubContext;

        public SignalRMessenger(IHubContext<SignalRHub> hubContext) {
          _hubContext = hubContext;
        }

        public async void SendNotification(string message) {
          await _hubContext.Clients.All.SendAsync("ReceiveNotification", "", message);
        } 
    }
}