using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("Section12ApprovalStatusesAudit")]
  public partial class Section12ApprovalStatusAudit : NameDescriptionAudit, ISection12ApprovalStatus
  {
  }
}
