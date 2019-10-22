using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class SpecialitySeeder : SeederBase<Speciality>
  {
    #region Constants
    internal const string DESCRIPTION_SECTION_12 = "Section 12 Description";
    internal const string NAME_SECTION_12 = "Section 12";
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.Speciality.SECTION_12,
        NAME_SECTION_12,
        DESCRIPTION_SECTION_12
      );
    }
  }
}