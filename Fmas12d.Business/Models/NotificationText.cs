using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace Fmas12d.Business.Models
{
  public class NotificationText : NameDescription
  {
    public NotificationText(Data.Entities.NotificationText entity) : base(entity) {
      if (entity == null) return;
      MessageTemplate = entity.MessageTemplate;
    }


    public const int SELECTED_FOR_ASSESSMENT = 1;
    public const int ALLOCATED_TO_ASSESSMENT = 2;
    public const int ASSESSMENT_CANCELLED = 3;
    public const int ASSESSMENT_UPDATED = 4;
    public const int NOT_ALLOCATED_TO_ASSESSMENT = 5;
    public const int REMOVED_FROM_ASSESSMENT = 6;
    public const int ASSESSMENT_SCHEDULED = 7;
    public const int CLAIM_STATUS_UPDATED = 8;
    public const int ASSESSMENT_COMPLETED = 9;
    
    [MaxLength(2000)]
    [Required]
    public string MessageTemplate { get; set; }
    public virtual IList<UserAssessmentNotification> UserAssessmentNotifications { get; set; }
  }
}