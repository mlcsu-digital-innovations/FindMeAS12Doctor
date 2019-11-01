using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("UserSpecialitiesAudit")]
  public partial class UserSpecialityAudit : BaseAudit, IUserSpeciality
  {    
    public int SpecialityId { get; set; }
    public int UserId { get; set; }
  }
}
