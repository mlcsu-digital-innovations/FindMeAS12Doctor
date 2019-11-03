using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("Section12ApprovalStatusesAudit")]
  public partial class Section12ApprovalStatusAudit : 
    NameDescriptionAudit, ISection12ApprovalStatus
  {
  }
}
