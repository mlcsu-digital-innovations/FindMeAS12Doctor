using System.Linq;
using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class ProfileTypesSeeder : SeederBase<ProfileType>
  {
    #region
    internal const string DESCRIPTION_AMHP = "AMHP Description";
    internal const string DESCRIPTION_DOCTOR = "Doctor Description";
    internal const string DESCRIPTION_FINANCE = "Finance Description";
    internal const string DESCRIPTION_SYSTEM = "System Description";
    internal const string NAME_AMHP = "AMHP";
    internal const string NAME_DOCTOR = "Doctor";
    internal const string NAME_FINANCE = "Finance";
    internal const string NAME_SYSTEM = "System";      
    #endregion

    internal void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.ProfileType.AMHP,
        NAME_AMHP,
        DESCRIPTION_AMHP
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ProfileType.DOCTOR,
        NAME_DOCTOR,
        DESCRIPTION_DOCTOR
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ProfileType.FINANCE,
        NAME_FINANCE,
        DESCRIPTION_FINANCE
      );

      SaveChangesWithIdentity();
    }

    /// <summary>
    /// Deletes all seeds except for Id = 1 which is required for the system account
    /// </summary>
    internal override void DeleteSeeds()
    {
      Context.ProfileTypes.RemoveRange(
        Context.ProfileTypes.Where(u => u.Id != 1).ToList()
      );

      ResetIdentity(1);
    }
  }
}