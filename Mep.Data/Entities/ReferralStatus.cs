using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public partial class ReferralStatus : NameDescription, IReferralStatus
  {
    public virtual IList<ReferralStatus> ReferralStatuses { get; set; }
  }
}
