using System.Linq;
using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
  internal class SystemAdminUserSeeder : SeederBase
  {

    internal SystemAdminUserSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      User systemAdminUser;

      if ((systemAdminUser = GetSystemAdminUser()) == null)
      {
        systemAdminUser = new User();
        _context.Add(systemAdminUser);
      }
      systemAdminUser.HasReadTermsAndConditions = true;
      systemAdminUser.IdentityServerIdentifier = SystemAdminIdentityServerIdentifier;
      systemAdminUser.IsActive = true;
      systemAdminUser.ModifiedAt = _now;
      systemAdminUser.DisplayName = "System Admin User";

      Organisation systemOrganisation;
      if ((systemOrganisation =
        _context.Organisations
          .SingleOrDefault(o => o.Name == "System Organisation")) == null)
      {
        systemOrganisation = new Organisation();
        _context.Add(systemOrganisation);
      }

      systemOrganisation.Description = "System Organisation Description";
      systemOrganisation.IsActive = false;
      systemOrganisation.ModifiedAt = _now;
      systemOrganisation.Name = "System Organisation";

      systemAdminUser.Organisation = systemOrganisation;

      ProfileType systemProfileType;
      if ((systemProfileType =
        _context.ProfileTypes
          .SingleOrDefault(o => o.Name == "System ProfileType")) == null)
      {
        systemProfileType = new ProfileType();
        _context.Add(systemProfileType);
      }

      systemProfileType.Description = "System ProfileType Description";
      systemProfileType.IsActive = false;
      systemProfileType.ModifiedAt = _now;
      systemProfileType.Name = "System ProfileType";

      systemAdminUser.ProfileType = systemProfileType;

      _context.SaveChanges();

      systemAdminUser.ModifiedByUser = systemAdminUser;
      systemOrganisation.ModifiedByUser = systemAdminUser;
      systemProfileType.ModifiedByUser = systemAdminUser;

    }
  }
}