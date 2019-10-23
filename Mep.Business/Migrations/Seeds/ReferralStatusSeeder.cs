using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class ReferralStatusSeeder : SeederBase<ReferralStatus>
  {
    #region Constants
    internal const string DESCRIPTION_ALLOCATED_DOCTORS = "Allocated Doctors Description";    
    internal const string DESCRIPTION_ALLOCATING_DOCTORS = "Allocating Doctors Description";    
    internal const string DESCRIPTION_ASSIGNING_DOCTORS = "Assigning Doctors Description";
    internal const string DESCRIPTION_NEW = "New Description";
    
    internal const string NAME_ALLOCATED_DOCTORS = "Allocated Doctors";
    internal const string NAME_ALLOCATING_DOCTORS = "Allocating Doctors";
    internal const string NAME_ASSIGNING_DOCTORS = "Assigning Doctors";
    internal const string NAME_NEW = "New";
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.NEW,
        NAME_NEW,
        DESCRIPTION_NEW
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.ASSIGNING_DOCTORS,
        NAME_ASSIGNING_DOCTORS,
        DESCRIPTION_ASSIGNING_DOCTORS        
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.ALLOCATING_DOCTORS,
        NAME_ALLOCATING_DOCTORS,
        DESCRIPTION_ALLOCATING_DOCTORS
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ReferralStatus.ALLOCATED_DOCTORS,
        NAME_ALLOCATED_DOCTORS,
        DESCRIPTION_ALLOCATED_DOCTORS
      );      
    }
  }
}