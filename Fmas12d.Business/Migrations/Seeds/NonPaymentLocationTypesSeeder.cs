using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class NonPaymentLocationTypesSeeder : SeederBase<NonPaymentLocationType>
  {
    #region 
    internal const string DESCRIPTION_GP_PRACTICE = "GP Practice Description";
    internal const string DESCRIPTION_HOSPITAL = "Hospital Description";
    internal const string NAME_GP_PRACTICE = "GP Practice";
    internal const string NAME_HOSPITAL = "Hospital";
    #endregion    
    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.NonPaymentLocationType.GP_PRACTICE,
        NAME_GP_PRACTICE,
        DESCRIPTION_GP_PRACTICE
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.NonPaymentLocationType.HOSPITAL,
        NAME_HOSPITAL,
        DESCRIPTION_HOSPITAL
      );

      SaveChangesWithIdentity();  
    }
  }
}