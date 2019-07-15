using System;

namespace Mep.Data.Entities
{
  public partial class UserExaminationNotification : BaseEntity, IUserExaminationNotification
  {
    public virtual Examination Examination { get; set; }
    public int ExaminationId { get; set; }
    public bool HasAccepted { get; set; }
    public bool HasResponded { get; set; }
    public virtual NotificationText NotificationText { get; set; }
    public int NotificationTextId { get; set; }
    public DateTimeOffset? ResponsedAt { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
  }
}
