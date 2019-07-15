using System;

namespace Mep.Data.Entities
{
  public partial class UserExaminationNotification : BaseEntity, IUserExaminationNotification
  {
    public virtual IExamination Examination { get; set; }
    public int ExaminationId { get; set; }
    public bool HasAccepted { get; set; }
    public bool HasResponded { get; set; }
    public virtual INotificationText NotificationText { get; set; }
    public int NotificationTextId { get; set; }
    public DateTimeOffset? ResponsedAt { get; set; }
    public virtual IUser User { get; set; }
    public int UserId { get; set; }
  }
}
