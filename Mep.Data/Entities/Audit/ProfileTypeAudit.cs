using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("ProfileTypesAudit")]
  public class ProfileTypeAudit : NameDescriptionAudit, IProfileType
  {
    // public virtual IList<UserAudit> Users { get; set; }
  }
}