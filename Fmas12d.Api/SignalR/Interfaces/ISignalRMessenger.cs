namespace Fmas12d.Api.SignalR
{
    public interface ISignalRMessenger
    {
        void SendNotification(string message, int profileType);
    }
}