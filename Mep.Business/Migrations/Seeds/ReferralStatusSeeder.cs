using System.Linq;
using Mep.Data.Entities;

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

      if ((referralStatus =
        _context.ReferralStatuses
          .SingleOrDefault(g => g.Name == "New Referral")) == null)
      {
        referralStatus = new ReferralStatus();
        _context.Add(referralStatus);
      }

      referralStatus.IsActive = true;
      referralStatus.ModifiedAt = _now;
      referralStatus.ModifiedByUser = GetSystemAdminUser();
      referralStatus.Name = "New Referral";
      referralStatus.Description = "New Referral";
    }
  }
}