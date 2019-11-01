using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("UserExaminationNotificationsAudit")]
  public partial class UserExaminationNotificationAudit : 
    BaseAudit, IUserExaminationNotification
  {
    public int ExaminationId { get; set; }
    public bool? HasAccepted { get; set; }
    public int NotificationTextId { get; set; }
    public DateTimeOffset? RespondedAt { get; set; }
    public int UserId { get; set; }
  }
}
