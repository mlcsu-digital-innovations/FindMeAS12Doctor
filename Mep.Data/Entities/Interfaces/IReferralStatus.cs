using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface IReferralStatus
  {
    IList<IReferralStatus> ReferralStatuses { get; set; }
  }
}