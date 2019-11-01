using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("ProfileTypesAudit")]
  public class ProfileTypeAudit : NameDescriptionAudit, IProfileType
  {
  }
}