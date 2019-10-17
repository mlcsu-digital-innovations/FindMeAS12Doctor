using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class NotificationText : NameDescription
  {
    public const int ASSIGNED_TO_EXAMINATION = 1;
    
    [MaxLength(2000)]
    [Required]
    public string MessageTemplate { get; set; }
    public virtual IList<UserExaminationNotification> UserExaminationNotifications { get; set; }
  }
}