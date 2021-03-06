using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class AssessmentDoctorStatusSeeder : SeederBase<AssessmentDoctorStatus>
  {
    #region Constants
    internal const string DESCRIPTION_ALLOCATED = "Allocated Description";    
    internal const string DESCRIPTION_ATTENDED = "Attended Description";            
    internal const string DESCRIPTION_SELECTED = "Selected Description";
    internal const string DESCRIPTION_NOT_ALLOCATED = "Not Allocated Description";
    internal const string DESCRIPTION_NOT_ATTENDED = "Not Attended Description";    
    internal const string DESCRIPTION_REMOVED = "Removed Description";
    internal const string NAME_ALLOCATED = "Allocated";
    internal const string NAME_ATTENDED = "Attended";
    internal const string NAME_SELECTED = "Selected";
    internal const string NAME_NOT_ALLOCATED = "Not Allocated";
    internal const string NAME_NOT_ATTENDED = "Not Attended";    
    internal const string NAME_REMOVED = "Removed";

    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.AssessmentDoctorStatus.SELECTED,
        NAME_SELECTED,
        DESCRIPTION_SELECTED
      );
      
      AddOrUpdateNameDescriptionEntityById(
        Models.AssessmentDoctorStatus.ALLOCATED,
        NAME_ALLOCATED,
        DESCRIPTION_ALLOCATED
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.AssessmentDoctorStatus.ATTENDED,
        NAME_ATTENDED,
        DESCRIPTION_ATTENDED
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.AssessmentDoctorStatus.NOT_ALLOCATED,
        NAME_NOT_ALLOCATED,
        DESCRIPTION_NOT_ALLOCATED
      );      

      AddOrUpdateNameDescriptionEntityById(
        Models.AssessmentDoctorStatus.NOT_ATTENDED,
        NAME_NOT_ATTENDED,
        DESCRIPTION_NOT_ATTENDED
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.AssessmentDoctorStatus.REMOVED,
        NAME_REMOVED,
        DESCRIPTION_REMOVED
      );      
      SaveChangesWithIdentity();
    }
  }
}