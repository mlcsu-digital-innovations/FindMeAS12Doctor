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


    // private void AddOrUpdate(int id, string name, string description)
    // {
    //   GenderType genderType;

    //   if ((genderType = _context.GenderTypes.Find(id)) == null)
    //   {
    //     genderType = new GenderType();
    //     _context.Add(genderType);
    //   }
    //   PopulateNameDescriptionActiveAndModifiedWithSystemUser(
    //     genderType,
    //     name,
    //     description
    //   );
    // }
  }
}