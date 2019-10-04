using Mep.Data.Entities;
using System.Linq;

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
      systemAdminUser.IdentityServerIdentifier = SYSTEM_ADMIN_IDENTITY_SERVER_IDENTIFIER;
      systemAdminUser.IsActive = true;
      systemAdminUser.ModifiedAt = _now;
      systemAdminUser.DisplayName = USER_DISPLAY_NAME_SYSTEM_ADMIN;

      Organisation systemOrganisation;
      if ((systemOrganisation = _context
        .Organisations
          .SingleOrDefault(o => o.Name == ORGANISATION_NAME_SYSTEM_ADMIN))
            == null)
      {
        systemOrganisation = new Organisation();
        _context.Add(systemOrganisation);
      }
      systemOrganisation.Description = ORGANISATION_DESCRIPTION_SYSTEM_ADMIN;
      systemOrganisation.IsActive = false;
      systemOrganisation.ModifiedAt = _now;
      systemOrganisation.Name = ORGANISATION_NAME_SYSTEM_ADMIN;

      systemAdminUser.Organisation = systemOrganisation;

      ProfileType systemProfileType;
      if ((systemProfileType = _context
        .ProfileTypes
          .SingleOrDefault(o => o.Name == PROFILE_TYPE_NAME_SYSTEM))
            == null)
      {
        systemProfileType = new ProfileType();
        _context.Add(systemProfileType);
      }
      systemProfileType.Description = PROFILE_TYPE_DESCRIPTION_SYSTEM;
      systemProfileType.IsActive = false;
      systemProfileType.ModifiedAt = _now;
      systemProfileType.Name = PROFILE_TYPE_NAME_SYSTEM;

      systemAdminUser.ProfileType = systemProfileType;

      _context.SaveChanges();

      systemAdminUser.ModifiedByUser = systemAdminUser;
      systemOrganisation.ModifiedByUser = systemAdminUser;
      systemProfileType.ModifiedByUser = systemAdminUser;

    }
  }
}