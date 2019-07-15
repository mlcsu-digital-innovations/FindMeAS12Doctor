using System;

namespace Mep.Data.Entities
{
  public interface IUserExaminationNotification
  {
    int ExaminationId { get; set; }
    bool HasAccepted { get; set; }
    bool HasResponded { get; set; }
    int NotificationTextId { get; set; }
    DateTimeOffset? ResponsedAt { get; set; }
    int UserId { get; set; }
  }
}