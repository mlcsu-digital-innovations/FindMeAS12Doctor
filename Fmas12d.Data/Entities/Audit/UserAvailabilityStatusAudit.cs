using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("UserAvailabilityStatusesAudit")]
  public partial class UserAvailabilityStatusAudit : 
    NameDescriptionAudit, IUserAvailabilityStatus
  {
  }
}
