using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("NonPaymentLocationTypesAudit")]
  public partial class NonPaymentLocationTypeAudit : NameDescriptionAudit, INonPaymentLocationType
  {
  }
}
