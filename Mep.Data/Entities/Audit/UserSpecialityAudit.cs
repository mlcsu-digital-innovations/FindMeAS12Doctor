using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("UserSpecialitiesAudit")]
  public partial class UserSpecialityAudit : BaseAudit, IUserSpeciality
  {    
    // public virtual SpecialityAudit Speciality { get; set; }
    public int SpecialityId { get; set; }
    // public virtual UserAudit User { get; set; }
    public int UserId { get; set; }
  }
}
