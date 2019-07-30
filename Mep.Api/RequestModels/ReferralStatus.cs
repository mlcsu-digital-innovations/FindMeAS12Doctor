using System.Collections.Generic;

namespace Mep.Api.RequestModels
{
    public class ReferralStatus : NameDescription
    {
        public virtual IList<ReferralStatus> ReferralStatuses { get; set; }
    }
}