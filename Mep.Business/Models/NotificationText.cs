using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class NotificationText : NameDescription
  {
    [MaxLength(2000)]
    [Required]
    public string MessageTemplate { get; set; }
    public virtual IList<UserExaminationNotification> UserExaminationNotifications { get; set; }
  }
}