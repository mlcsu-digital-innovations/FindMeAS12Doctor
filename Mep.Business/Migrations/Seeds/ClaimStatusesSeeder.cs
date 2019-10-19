using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class ClaimStatusesSeeder : SeederBase<ClaimStatus>
  {
    #region Constants
    internal const string DESCRIPTION_ACCEPTED = "Accepted Description";
    internal const string NAME_ACCEPTED = "Accepted";    
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntity(
        Models.ClaimStatus.ACCEPTED,
        NAME_ACCEPTED,
        DESCRIPTION_ACCEPTED
      );
    }
  }
}