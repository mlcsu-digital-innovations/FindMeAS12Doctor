using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class UserAvailabilityStatusSeeder : SeederBase<UserAvailabilityStatus>
  {
    #region Constants
    internal const string DESCRIPTION_AVAILABLE = "Available Description";
    internal const string DESCRIPTION_ON_CALL = "On Call Description";
    internal const string DESCRIPTION_UNAVAILABLE = "Unavailable Description";
    internal const string NAME_AVAILABLE = "Available";
    internal const string NAME_ON_CALL = "On Call";
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

      AddOrUpdateNameDescriptionEntityById(
        Models.UserAvailabilityStatus.ON_CALL, NAME_ON_CALL, DESCRIPTION_ON_CALL
      );      

      SaveChangesWithIdentity();
    }
  }
}