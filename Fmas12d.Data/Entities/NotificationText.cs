using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Data.Entities
{
  public partial class NotificationText : NameDescription, INotificationText
  {
    [MaxLength(2000)]
    [Required]
    public string MessageTemplate { get; set; }
    public virtual IList<UserAssessmentNotification> UserAssessmentNotifications { get; set; }
  }
}
