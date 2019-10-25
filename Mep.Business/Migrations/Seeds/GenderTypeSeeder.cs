using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class GenderTypesSeeder : SeederBase<GenderType>
  {
    #region Constants
    internal const string DESCRIPTION_FEMALE = "Female Description";
    internal const string DESCRIPTION_MALE = "Male Description";
    internal const string DESCRIPTION_OTHER = "Other Description";
    internal const string NAME_FEMALE = "Female";
    internal const string NAME_MALE = "Male";
    internal const string NAME_OTHER = "Other";
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.GenderType.FEMALE, NAME_FEMALE, DESCRIPTION_FEMALE
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.GenderType.MALE, NAME_MALE, DESCRIPTION_MALE
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.GenderType.OTHER, NAME_OTHER, DESCRIPTION_OTHER
      );

      SaveChangesWithIdentity();
    }
  }
}