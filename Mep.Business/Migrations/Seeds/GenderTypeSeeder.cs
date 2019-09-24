using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class GenderTypeSeeder : SeederBase
  {
    internal GenderTypeSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      GenderType genderType;

      if ((genderType =
        _context.GenderTypes
          .SingleOrDefault(g => g.Name == MALE)) == null)
      {
        genderType = new GenderType();
        _context.Add(genderType);
      }

      genderType.IsActive = true;
      genderType.ModifiedAt = _now;
      genderType.ModifiedByUser = GetSystemAdminUser();
      genderType.Name = MALE;
      genderType.Description = MALE;

      if ((genderType =
        _context.GenderTypes
          .SingleOrDefault(g => g.Name == FEMALE)) == null)
      {
        genderType = new GenderType();
        _context.Add(genderType);
      }

      genderType.IsActive = true;
      genderType.ModifiedAt = _now;
      genderType.ModifiedByUser = GetSystemAdminUser();
      genderType.Name = FEMALE;
      genderType.Description = FEMALE;

      if ((genderType =
        _context.GenderTypes
          .SingleOrDefault(g => g.Name == OTHER)) == null)
      {
        genderType = new GenderType();
        _context.Add(genderType);
      }

      genderType.IsActive = true;
      genderType.ModifiedAt = _now;
      genderType.ModifiedByUser = GetSystemAdminUser();
      genderType.Name = OTHER;
      genderType.Description = OTHER;
    }
  }
}