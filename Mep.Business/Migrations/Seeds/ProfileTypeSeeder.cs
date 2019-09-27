using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  internal class ProfileTypeSeeder : SeederBase
  {
    internal ProfileTypeSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      ProfileType profileType;

      if (
        (profileType = _context.ProfileTypes.SingleOrDefault(u => u.Name == PROFILE_TYPE_1)) == null)

      {
        profileType = new ProfileType();
        _context.Add(profileType);
      }

      profileType.Description = PROFILE_TYPE_1;
      profileType.IsActive = true;
      profileType.ModifiedAt = _now;
      profileType.ModifiedByUser = GetSystemAdminUser();
      profileType.Name = PROFILE_TYPE_1;

      if (
        (profileType = _context.ProfileTypes.SingleOrDefault(u => u.Name == PROFILE_TYPE_2)) == null)

      {
        profileType = new ProfileType();
        _context.Add(profileType);
      }

      profileType.Description = PROFILE_TYPE_2;
      profileType.IsActive = true;
      profileType.ModifiedAt = _now;
      profileType.ModifiedByUser = GetSystemAdminUser();
      profileType.Name = PROFILE_TYPE_2;

      if (
        (profileType = _context.ProfileTypes.SingleOrDefault(u => u.Name == PROFILE_TYPE_3)) == null)

      {
        profileType = new ProfileType();
        _context.Add(profileType);
      }

      profileType.Description = PROFILE_TYPE_3;
      profileType.IsActive = true;
      profileType.ModifiedAt = _now;
      profileType.ModifiedByUser = GetSystemAdminUser();
      profileType.Name = PROFILE_TYPE_3;

    }
  }
}