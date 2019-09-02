using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("UserExaminationNotificationsAudit")]
  public partial class UserExaminationNotificationAudit : BaseAudit, IUserExaminationNotification
  {
    // public virtual ExaminationAudit Examination { get; set; }
    public int ExaminationId { get; set; }
    public bool? HasAccepted { get; set; }
    // public virtual NotificationTextAudit NotificationText { get; set; }
    public int NotificationTextId { get; set; }
    public DateTimeOffset? RespondedAt { get; set; }
    // public virtual UserAudit User { get; set; }
    public int UserId { get; set; }
  }
}
