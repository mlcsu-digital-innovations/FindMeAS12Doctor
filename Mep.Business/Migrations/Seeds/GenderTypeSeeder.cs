using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class GenderTypeSeeder : SeederBase<GenderType>
  {
    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.GenderType.FEMALE, GENDER_TYPE_NAME_FEMALE, GENDER_TYPE_DESCRIPTION_FEMALE);

      AddOrUpdateNameDescriptionEntityById(
        Models.GenderType.MALE, GENDER_TYPE_NAME_MALE, GENDER_TYPE_DESCRIPTION_MALE);

      AddOrUpdateNameDescriptionEntityById(
        Models.GenderType.OTHER, GENDER_TYPE_NAME_OTHER, GENDER_TYPE_DESCRIPTION_OTHER);
    }
  }
}