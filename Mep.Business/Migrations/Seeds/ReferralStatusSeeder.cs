using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ReferralStatusSeeder : SeederBase<ReferralStatus>
  {
   internal void SeedData()
    {
      ReferralStatus referralStatus;

      if ((referralStatus = _context
        .ReferralStatuses
          .SingleOrDefault(g => g.Name ==
            REFERRAL_STATUS_NAME_NEW_REFERRAL)) == null)
      {
        referralStatus = new ReferralStatus();
        _context.Add(referralStatus);
      }
      referralStatus.Description =
        REFERRAL_STATUS_DESCRIPTION_NEW_REFERRAL;
      referralStatus.IsActive = true;
      referralStatus.ModifiedAt = _now;
      referralStatus.ModifiedByUser = GetSystemAdminUser();
      referralStatus.Name = REFERRAL_STATUS_NAME_NEW_REFERRAL;
    }
  }
}