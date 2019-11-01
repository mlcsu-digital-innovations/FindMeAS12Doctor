using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("GenderTypesAudit")]
  public class GenderTypeAudit : NameDescriptionAudit, IGenderType
  {
  }
}