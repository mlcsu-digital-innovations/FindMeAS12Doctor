using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class ReferralStatus : NameDescription
  {
    public virtual IList<ReferralStatus> ReferralStatuses { get; set; }
  }
}