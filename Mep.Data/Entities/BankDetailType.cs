using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public partial class BankDetailType : NameDescription, IBankDetailType
  {
    public virtual IList<BankDetail> BankDetails { get; set; }
  }
}
