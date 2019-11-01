using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("NotificationTextsAudit")]
  public partial class NotificationTextAudit : NameDescriptionAudit, INotificationText
  {
    [MaxLength(2000)]
    [Required]
    public string MessageTemplate { get; set; }
  }
}
