using System;

namespace Fmas12d.Data.Entities
{
    public class UserNotificationEmail: BaseEntity, IUserNotificationEmail
    {
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string EmailContent { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public DateTimeOffset? DateSent { get; set; }
        public virtual NotificationEmail NotificationEmail { get; set; }
        public int NotificationEmailId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}