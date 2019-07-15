using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface INotificationText
  {
    string MessageTemplate { get; set; }
    IList<IUserExaminationNotification> UserExaminationNotifications { get; set; }
  }
}