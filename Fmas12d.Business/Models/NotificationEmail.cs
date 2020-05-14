using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Fmas12d.Business.Models
{
  public class NotificationEmail : NameDescription
  {
    public NotificationEmail(Data.Entities.NotificationEmail entity) : base(entity) {
      if (entity == null) return;
      MessageTemplate = entity.MessageTemplate;
      SubjectTemplate = entity.SubjectTemplate;
    }

    public const int MISSING_VSR_NUMBER = 1;
    
    [MaxLength(2000)]
    [Required]
    public string MessageTemplate { get; set; }
    [MaxLength(255)]
    [Required]
    public string SubjectTemplate { get; set; }
    public virtual IList<UserNotificationEmail> UserNotificationEmails { get; set; }
  }
}