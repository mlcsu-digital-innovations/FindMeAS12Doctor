using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ClaimStatusesSeeder : SeederBase
  {

    internal ClaimStatusesSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      ClaimStatus claimStatus;

      if ((claimStatus = _context
        .ClaimStatuses
          .SingleOrDefault(g => g.Name == CLAIM_STATUS_NAME_ACCEPTED))
            == null)
      {
        claimStatus = new ClaimStatus();
        _context.Add(claimStatus);
      }
      claimStatus.Description = CLAIM_STATUS_DESCRIPTION_ACCEPTED;
      claimStatus.IsActive = true;
      claimStatus.ModifiedAt = _now;
      claimStatus.ModifiedByUser = GetSystemAdminUser();
      claimStatus.Name = CLAIM_STATUS_NAME_ACCEPTED;
    }
  }
}