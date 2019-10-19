using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class GenderTypeSeeder : SeederBase<GenderType>
  {
    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntity(
        Models.GenderType.FEMALE, GENDER_TYPE_NAME_FEMALE, GENDER_TYPE_DESCRIPTION_FEMALE);

      AddOrUpdateNameDescriptionEntity(
        Models.GenderType.MALE, GENDER_TYPE_NAME_MALE, GENDER_TYPE_DESCRIPTION_MALE);

      AddOrUpdateNameDescriptionEntity(
        Models.GenderType.OTHER, GENDER_TYPE_NAME_OTHER, GENDER_TYPE_DESCRIPTION_OTHER);
    }
  }
}