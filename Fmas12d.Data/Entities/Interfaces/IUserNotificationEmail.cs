using System;

namespace Fmas12d.Data.Entities
{
    public interface IUserNotificationEmail
    {
         string ToAddress { get; set; }
         string Subject { get; set; }
         string EmailContent { get; set; }
         DateTimeOffset DateAdded { get; set; }
         DateTimeOffset? DateSent { get; set; }
         int NotificationEmailId { get; set; }
         int UserId { get; set; }
    }
}