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

      if ((genderType = _context
        .GenderTypes
          .SingleOrDefault(g => g.Name == GENDER_TYPE_NAME_MALE))
            == null)
      {
        genderType = new GenderType();
        _context.Add(genderType);
      }
      genderType.IsActive = true;
      genderType.ModifiedAt = _now;
      genderType.ModifiedByUser = GetSystemAdminUser();
      genderType.Name = GENDER_TYPE_NAME_MALE;
      genderType.Description = GENDER_TYPE_DESCRIPTION_MALE;

      if ((genderType = _context
        .GenderTypes
          .SingleOrDefault(g => g.Name == GENDER_TYPE_NAME_FEMALE))
            == null)
      {
        genderType = new GenderType();
        _context.Add(genderType);
      }
      genderType.IsActive = true;
      genderType.ModifiedAt = _now;
      genderType.ModifiedByUser = GetSystemAdminUser();
      genderType.Name = GENDER_TYPE_NAME_FEMALE;
      genderType.Description = GENDER_TYPE_DESCRIPTION_FEMALE;

      if ((genderType = _context.GenderTypes
        .SingleOrDefault(g => g.Name == GENDER_TYPE_NAME_OTHER)) == null)
      {
        genderType = new GenderType();
        _context.Add(genderType);
      }
      genderType.IsActive = true;
      genderType.ModifiedAt = _now;
      genderType.ModifiedByUser = GetSystemAdminUser();
      genderType.Name = GENDER_TYPE_NAME_OTHER;
      genderType.Description = GENDER_TYPE_DESCRIPTION_OTHER;
    }
  }
}