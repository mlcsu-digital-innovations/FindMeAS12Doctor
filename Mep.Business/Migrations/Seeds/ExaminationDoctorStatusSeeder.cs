using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class ExaminationDoctorStatusSeeder : SeederBase<ExaminationDoctorStatus>
  {
    #region Constants
    internal const string DESCRIPTION_ALLOCATED = "Allocated Description";    
    internal const string DESCRIPTION_ATTENDED = "Attended Description";            
    internal const string DESCRIPTION_SELECTED = "Selected Description";
    internal const string NAME_ALLOCATED = "Allocated";
    internal const string NAME_ATTENDED = "Attended";
    internal const string NAME_SELECTED = "Selected";

    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.ExaminationDoctorStatus.SELECTED,
        NAME_SELECTED,
        DESCRIPTION_SELECTED
      );
      
      AddOrUpdateNameDescriptionEntityById(
        Models.ExaminationDoctorStatus.ALLOCATED,
        NAME_ALLOCATED,
        DESCRIPTION_ALLOCATED
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ExaminationDoctorStatus.ATTENDED,
        NAME_ATTENDED,
        DESCRIPTION_ATTENDED
      );

      SaveChangesWithIdentity();
    }
  }
}