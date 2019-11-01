using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Fmas12d.Business.Models
{
  public class NotificationText : NameDescription
  {
    public const int SELECTED_FOR_EXAMINATION = 1;
    public const int ALLOCATED_TO_EXAMINATION = 2;
    public const int EXAMINATION_CANCELLED = 3;
    
    [MaxLength(2000)]
    [Required]
    public string MessageTemplate { get; set; }
    public virtual IList<UserExaminationNotification> UserExaminationNotifications { get; set; }
  }
}