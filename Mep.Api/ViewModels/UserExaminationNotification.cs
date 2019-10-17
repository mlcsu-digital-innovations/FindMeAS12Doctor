using System;

namespace Mep.Api.ViewModels
{
  public class UserExaminationNotification
  {
    public int ExaminationId { get; set; }
    public bool? HasAccepted { get; set; }
    public int NotificationTextId { get; set; }
    public DateTimeOffset? RespondedAt { get; set; }
    public int UserId { get; set; }
  }
}