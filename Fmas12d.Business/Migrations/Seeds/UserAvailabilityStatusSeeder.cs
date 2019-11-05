using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class UserAvailabilityStatusSeeder : SeederBase<UserAvailabilityStatus>
  {
    #region Constants
    internal const string DESCRIPTION_AVAILABLE = "Available Description";
    internal const string DESCRIPTION_UNAVAILABLE = "Unavailable Description";
    internal const string NAME_AVAILABLE = "Available";
    internal const string NAME_UNAVAILABLE = "Unavailable";
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.UserAvailabilityStatus.AVAILABLE, NAME_AVAILABLE, DESCRIPTION_AVAILABLE
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.UserAvailabilityStatus.UNAVAILABLE, NAME_UNAVAILABLE, DESCRIPTION_UNAVAILABLE
      );

      SaveChangesWithIdentity();
    }
  }
}