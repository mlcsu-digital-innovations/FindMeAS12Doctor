using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Data.Entities
{
  public partial class NotificationEmail : NameDescription, INotificationEmail
  {
    [Required]
    public string MessageTemplate { get; set; }
    [MaxLength(255)]
    [Required]
    public string SubjectTemplate { get; set; }
    public virtual IList<UserNotificationEmail> UserNotificationEmails { get; set; }
  }
}