using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("BankDetailTypesAudit")]
  public partial class BankDetailTypeAudit : BaseAudit, IBankDetailType
  {
    // public virtual IList<BankDetailAudit> BankDetails { get; set; }
  }
}
