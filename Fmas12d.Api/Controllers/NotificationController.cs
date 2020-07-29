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
        private ISignalRMessenger _signalRMessenger;

        public NotificationController(ISignalRMessenger signalRMessenger) {
          _signalRMessenger = signalRMessenger;
        }

    [HttpPost]
    [Route("{profileType:int}")]
    public void TestNotification(
      int profileType,
      [FromBody] string message
    )
    {
      _signalRMessenger.SendNotification(message, profileType);
    }
    }
}