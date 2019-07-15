using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mep.Data.Entities
{
  public partial class NotificationText : NameDescription, INotificationText
  {
    [MaxLength(2000)]
    [Required]
    public string MessageTemplate { get; set; }
    public virtual IList<UserExaminationNotification> UserExaminationNotifications { get; set; }
  }
}
