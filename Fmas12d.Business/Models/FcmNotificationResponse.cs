namespace Fmas12d.Business.Models
{
    public class FcmNotificationResponse
    {
        public string Multicast_Id { get; set; }
        public int Success { get; set; }
        public int Failure { get; set; }
        public int Canonical_Ids { get; set; }
        public MessageId[] Results { get; set; }
    }

    public class MessageId 
    {
      public string Message_Id { get; set; }
    }
}