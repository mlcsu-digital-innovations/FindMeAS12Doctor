using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("NotificationEmailsAudit")]
  public partial class NotificationEmailAudit : NameDescriptionAudit, INotificationEmail
  {
    [Required]
    public string MessageTemplate { get; set; }
    [MaxLength(255)]
    [Required]
    public string SubjectTemplate { get; set; }
  }
}