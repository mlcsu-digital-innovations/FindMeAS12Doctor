using System;

namespace Fmas12d.Api.ViewModels
{
  public class UserAssessmentNotification
  {
    public int AssessmentId { get; set; }
    public bool? HasAccepted { get; set; }
    public int NotificationTextId { get; set; }
    public DateTimeOffset? RespondedAt { get; set; }
    public int UserId { get; set; }
  }
}