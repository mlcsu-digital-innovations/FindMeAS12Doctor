using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class UnsuccessfulAssessmentTypesSeeder : SeederBase<UnsuccessfulAssessmentType>
  {
    #region Constants
    protected const string DESCRIPTION_REFUSED_ENTRY = "Refused Entry Description";
    protected const string DESCRIPTION_PATIENT_UNAVAILABLE = "Patient Unavailable Description";
    protected const string NAME_REFUSED_ENTRY = "Refused Entry";
    protected const string NAME_PATIENT_UNAVAILABLE = "Patient Unavailable";
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.UnsuccessfulAssessmentType.REFUSED_ENTRY,
        NAME_REFUSED_ENTRY,
        DESCRIPTION_REFUSED_ENTRY
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.UnsuccessfulAssessmentType.PATIENT_UNAVAILABLE,
        NAME_PATIENT_UNAVAILABLE,
        DESCRIPTION_PATIENT_UNAVAILABLE
      );

      SaveChangesWithIdentity();
    }
  }
}