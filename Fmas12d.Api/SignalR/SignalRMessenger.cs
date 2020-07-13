using Microsoft.AspNetCore.SignalR;
using Fmas12d.Business.Services;

namespace Fmas12d.Api.SignalR
{
    public class SignalRMessenger : ISignalRMessenger
    {
        private readonly IHubContext<SignalRHub> _hubContext;

        public SignalRMessenger(
          IHubContext<SignalRHub> hubContext
          ) {
          _hubContext = hubContext;
        }

        public async void SendNotification(string message, int profileType) {
          await _hubContext.Clients.All.SendAsync("ReceiveNotification", profileType, message);
        } 
    }
}