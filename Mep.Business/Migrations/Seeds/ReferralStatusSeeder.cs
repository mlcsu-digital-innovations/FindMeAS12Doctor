using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ReferralStatusSeeder : SeederBase
  {
    internal ReferralStatusSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      ReferralStatus referralStatus;

      if ((referralStatus = _context
        .ReferralStatuses
          .SingleOrDefault(g => g.Name == REFERRAL_STATUS_NAME_NEW_REFERRAL))
            == null)
      {
        referralStatus = new ReferralStatus();
        _context.Add(referralStatus);
      }
      referralStatus.IsActive = true;
      referralStatus.ModifiedAt = _now;
      referralStatus.ModifiedByUser = GetSystemAdminUser();
      referralStatus.Name = REFERRAL_STATUS_NAME_NEW_REFERRAL;
      referralStatus.Description = REFERRAL_STATUS_DESCRIPTION_NEW_REFERRAL;
    }
  }
}