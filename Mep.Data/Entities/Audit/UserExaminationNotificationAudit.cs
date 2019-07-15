using System;
using System.Collections.Generic;

namespace Mep.Data.Entities.Audit
{
  public partial class UserExaminationNotificationAudit : BaseAudit, IUserExaminationNotification
  {
    // public virtual ExaminationAudit Examination { get; set; }
    public int ExaminationId { get; set; }
    public bool HasAccepted { get; set; }
    public bool HasResponded { get; set; }
    // public virtual NotificationTextAudit NotificationText { get; set; }
    public int NotificationTextId { get; set; }
    public DateTimeOffset? ResponsedAt { get; set; }
    // public virtual UserAudit User { get; set; }
    public int UserId { get; set; }
  }
}
