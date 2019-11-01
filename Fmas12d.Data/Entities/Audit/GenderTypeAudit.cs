using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("GenderTypesAudit")]
  public class GenderTypeAudit : NameDescriptionAudit, IGenderType
  {
  }
}