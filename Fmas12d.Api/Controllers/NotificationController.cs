using Microsoft.AspNetCore.SignalR;
using Fmas12d.Api.SignalR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fmas12d.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
    
        private IHubContext<NotificationHub> _hubContext;

        public NotificationController(IHubContext<NotificationHub> hubContext) {
          _hubContext = hubContext;
        }

    [HttpGet]
    public async void TestNotification()
    {
      await _hubContext.Clients.All.SendAsync("ReceiveNotification", "fred", "bob");

      return;
    }
    }
}