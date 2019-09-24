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

      if ((referralStatus =
        _context.ReferralStatuses
          .SingleOrDefault(g => g.Name == REFERRALSTATUS)) == null)
      {
        referralStatus = new ReferralStatus();
        _context.Add(referralStatus);
      }

      referralStatus.IsActive = true;
      referralStatus.ModifiedAt = _now;
      referralStatus.ModifiedByUser = GetSystemAdminUser();
      referralStatus.Name = REFERRALSTATUS;
      referralStatus.Description = REFERRALSTATUS;
    }
  }
}