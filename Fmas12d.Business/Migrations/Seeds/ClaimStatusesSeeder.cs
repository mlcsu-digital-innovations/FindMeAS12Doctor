using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class ClaimStatusesSeeder : SeederBase<ClaimStatus>
  {
    #region Constants
    internal const string DESCRIPTION_ACCEPTED = "Accepted Description";
    internal const string NAME_ACCEPTED = "Accepted";    
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.ClaimStatus.ACCEPTED,
        NAME_ACCEPTED,
        DESCRIPTION_ACCEPTED
      );
      
      SaveChangesWithIdentity();
    }
  }
}