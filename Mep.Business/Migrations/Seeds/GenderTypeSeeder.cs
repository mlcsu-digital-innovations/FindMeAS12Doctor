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
          .SingleOrDefault(g => g.Name == GENDER_TYPE_MALE)) == null)
      {
        genderType = new GenderType();
        _context.Add(genderType);
      }

      genderType.IsActive = true;
      genderType.ModifiedAt = _now;
      genderType.ModifiedByUser = GetSystemAdminUser();
      genderType.Name = GENDER_TYPE_MALE;
      genderType.Description = GENDER_TYPE_MALE;

      if ((genderType =
        _context.GenderTypes
          .SingleOrDefault(g => g.Name == GENDER_TYPE_FEMALE)) == null)
      {
        genderType = new GenderType();
        _context.Add(genderType);
      }

      genderType.IsActive = true;
      genderType.ModifiedAt = _now;
      genderType.ModifiedByUser = GetSystemAdminUser();
      genderType.Name = GENDER_TYPE_FEMALE;
      genderType.Description = GENDER_TYPE_FEMALE;

      if ((genderType =
        _context.GenderTypes
          .SingleOrDefault(g => g.Name == GENDER_TYPE_OTHER)) == null)
      {
        genderType = new GenderType();
        _context.Add(genderType);
      }

      genderType.IsActive = true;
      genderType.ModifiedAt = _now;
      genderType.ModifiedByUser = GetSystemAdminUser();
      genderType.Name = GENDER_TYPE_OTHER;
      genderType.Description = GENDER_TYPE_OTHER;
    }
  }
}