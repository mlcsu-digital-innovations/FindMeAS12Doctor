using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class UnsuccessfulExaminationTypesSeeder : SeederBase<UnsuccessfulExaminationType>
  {
    #region Constants
    protected const string DESCRIPTION_REFUSED_ENTRY = "Refused Entry Description";
    protected const string NAME_REFUSED_ENTRY = "Refused Entry";
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.UnsuccessfulExaminationType.REFUSED_ENTRY,
        NAME_REFUSED_ENTRY,
        DESCRIPTION_REFUSED_ENTRY
      );

      SaveChangesWithIdentity();
    }
  }
}