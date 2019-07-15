using System;

namespace Mep.Data.Entities
{
  public interface IUserExaminationNotification
  {
    IExamination Examination { get; set; }
    int ExaminationId { get; set; }
    bool HasAccepted { get; set; }
    bool HasResponded { get; set; }
    INotificationText NotificationText { get; set; }
    int NotificationTextId { get; set; }
    DateTimeOffset? ResponsedAt { get; set; }
    IUser User { get; set; }
    int UserId { get; set; }
  }
}