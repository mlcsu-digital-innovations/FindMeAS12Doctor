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
        (profileType = _context.ProfileTypes.SingleOrDefault(u => u.Name == PROFILETYPE1)) == null)

      {
        profileType = new ProfileType();
        _context.Add(profileType);
      }

      profileType.Description = PROFILETYPE1;
      profileType.IsActive = true;
      profileType.ModifiedAt = _now;
      profileType.ModifiedByUser = GetSystemAdminUser();
      profileType.Name = PROFILETYPE1;

      if (
        (profileType = _context.ProfileTypes.SingleOrDefault(u => u.Name == PROFILETYPE2)) == null)

      {
        profileType = new ProfileType();
        _context.Add(profileType);
      }

      profileType.Description = PROFILETYPE2;
      profileType.IsActive = true;
      profileType.ModifiedAt = _now;
      profileType.ModifiedByUser = GetSystemAdminUser();
      profileType.Name = PROFILETYPE2;

      if (
        (profileType = _context.ProfileTypes.SingleOrDefault(u => u.Name == PROFILETYPE3)) == null)

      {
        profileType = new ProfileType();
        _context.Add(profileType);
      }

      profileType.Description = PROFILETYPE3;
      profileType.IsActive = true;
      profileType.ModifiedAt = _now;
      profileType.ModifiedByUser = GetSystemAdminUser();
      profileType.Name = PROFILETYPE3;

    }
  }
}