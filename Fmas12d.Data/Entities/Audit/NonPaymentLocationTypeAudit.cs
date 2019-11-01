using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("NonPaymentLocationTypesAudit")]
  public partial class NonPaymentLocationTypeAudit : NameDescriptionAudit, INonPaymentLocationType
  {
  }
}
