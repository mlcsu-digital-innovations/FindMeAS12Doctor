using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class ClaimStatusesSeeder : SeederBase
  {
    internal void SeedData()
    {
      ClaimStatus claimStatus;

      if ((claimStatus = _context.ClaimStatuses.Find(Models.ClaimStatus.ACCEPTED)) == null)
      {
        claimStatus = new ClaimStatus();
        _context.Add(claimStatus);
      }
      PopulateNameDescriptionActiveAndModifiedWithSystemUser(
        claimStatus, 
        CLAIM_STATUS_ACCEPTED_NAME, 
        CLAIM_STATUS_ACCEPTED_DESCRIPTION);
    }
  }
}