using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("UserAssessmentNotificationsAudit")]
  public partial class UserAssessmentNotificationAudit : 
    BaseAudit, IUserAssessmentNotification
  {
    public int AssessmentId { get; set; }
    public bool? HasAccepted { get; set; }
    public int NotificationTextId { get; set; }
    public DateTimeOffset? RespondedAt { get; set; }
    public int UserId { get; set; }
  }
}
