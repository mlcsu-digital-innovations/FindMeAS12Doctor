using System.Linq;
using Fmas12d.Data.Entities;

namespace Fmas12d.Business.Migrations.Seeds
{
  public class ProfileTypesSeeder : SeederBase<ProfileType>
  {
    #region Constants
    public const string DESCRIPTION_ADMIN = "Admin Description";
    public const string DESCRIPTION_AMHP = "AMHP Description";
    public const string DESCRIPTION_DOCTOR = "Doctor Description";
    public const string DESCRIPTION_FINANCE = "Finance Description";
    public const string DESCRIPTION_SYSTEM = "System Description";
    public const string DESCRIPTION_UNREGISTERED = "Unregistered Description";
    public const string NAME_ADMIN = "Admin";
    public const string NAME_AMHP = "AMHP";
    public const string NAME_DOCTOR = "Doctor";
    public const string NAME_FINANCE = "Finance";
    public const string NAME_SYSTEM = "System";
    public const string NAME_UNREGISTERED = "Unregistered";
    #endregion

    public void SeedData()
    {
      AddOrUpdateNameDescriptionEntityById(
        Models.ProfileType.ADMIN,
        NAME_ADMIN,
        DESCRIPTION_ADMIN
      );      

      AddOrUpdateNameDescriptionEntityById(
        Models.ProfileType.AMHP,
        NAME_AMHP,
        DESCRIPTION_AMHP
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ProfileType.GP,
        NAME_DOCTOR,
        DESCRIPTION_DOCTOR
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ProfileType.FINANCE,
        NAME_FINANCE,
        DESCRIPTION_FINANCE
      );

      AddOrUpdateNameDescriptionEntityById(
        Models.ProfileType.UNREGISTERED,
        NAME_UNREGISTERED,
        DESCRIPTION_UNREGISTERED
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