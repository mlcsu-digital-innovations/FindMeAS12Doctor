using System;

namespace Fmas12d.Data.Entities
{
  public interface IUserAssessmentNotification
  {
    int AssessmentId { get; set; }
    int NotificationTextId { get; set; }
    DateTimeOffset? SentAt { get; set; }
    int UserId { get; set; }
  }
}