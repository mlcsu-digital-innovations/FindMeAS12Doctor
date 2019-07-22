using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class BankDetailType : NameDescription
  {
    public virtual IList<BankDetail> BankDetails { get; set; }
  }
}