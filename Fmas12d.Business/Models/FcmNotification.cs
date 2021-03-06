namespace Fmas12d.Business.Models
{
    public class FcmNotification
    {
      public FcmNotification()
      {
        Notification = new NotificationBody();
        Data = new DataBody();

        Restricted_package_name = "";
        Notification.Click_action = "FCM_PLUGIN_ACTIVITY";
        Notification.Icon = "fcm_push_icon";
        Notification.Sound = "default";  
      }

        public NotificationBody Notification { get; set; }
        public DataBody Data { get; set; }
        public string To { get; set; }
        public string Priority { get; set; }
        public string Restricted_package_name { get; set; }
    }

    public class NotificationBody
    {
      public string Title { get; set; }
      public string Body { get; set; }
      public string Sound { get; set; }
      public string Click_action { get; set; }
      public string Icon { get; set; }
    }

    public class DataBody
    {
      public string NotificationTitle { get; set; }
      public string NotificationMessage { get; set; }
    }
}