using System;

namespace Fmas12d.Data.Entities
{
  public partial class UserAssessmentNotification : 
    BaseEntity, IUserAssessmentNotification
  {
    public virtual Assessment Assessment { get; set; }
    public int AssessmentId { get; set; }
    public virtual NotificationText NotificationText { get; set; }
    public int NotificationTextId { get; set; }
    public DateTimeOffset? SentAt { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }
  }
}
