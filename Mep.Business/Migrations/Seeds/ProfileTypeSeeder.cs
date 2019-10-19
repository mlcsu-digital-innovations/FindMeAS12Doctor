using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ProfileTypeSeeder : SeederBase<ProfileType>
  {
    internal void SeedData()
    {
      ProfileType profileType;

      if ((profileType = _context
        .ProfileTypes
          .SingleOrDefault(u => u.Name ==
            PROFILE_TYPE_NAME_AMHP)) == null)
      {
        profileType = new ProfileType();
        _context.Add(profileType);
      }
      profileType.Description = PROFILE_TYPE_DESCRIPTION_AMHP;
      profileType.IsActive = true;
      profileType.ModifiedAt = _now;
      profileType.ModifiedByUser = GetSystemAdminUser();
      profileType.Name = PROFILE_TYPE_NAME_AMHP;

      if ((profileType = _context
        .ProfileTypes
          .SingleOrDefault(u => u.Name ==
            PROFILE_TYPE_NAME_DOCTOR)) == null)
      {
        profileType = new ProfileType();
        _context.Add(profileType);
      }
      profileType.Description = PROFILE_TYPE_DESCRIPTION_DOCTOR;
      profileType.IsActive = true;
      profileType.ModifiedAt = _now;
      profileType.ModifiedByUser = GetSystemAdminUser();
      profileType.Name = PROFILE_TYPE_NAME_DOCTOR;

      if ((profileType = _context
        .ProfileTypes
          .SingleOrDefault(u => u.Name ==
            PROFILE_TYPE_NAME_FINANCE)) == null)
      {
        profileType = new ProfileType();
        _context.Add(profileType);
      }
      profileType.Description = PROFILE_TYPE_DESCRIPTION_FINANCE;
      profileType.IsActive = true;
      profileType.ModifiedAt = _now;
      profileType.ModifiedByUser = GetSystemAdminUser();
      profileType.Name = PROFILE_TYPE_NAME_FINANCE;
    }
  }
}