using System.Threading.Tasks;

namespace Fmas12d.Api.SignalR
{
    public interface ISignalRMessenger
    {
        void SendNotification(string message);
    }
}