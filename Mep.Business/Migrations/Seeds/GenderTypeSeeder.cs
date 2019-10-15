using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class GenderTypeSeeder : SeederBase
  {
    internal void SeedData()
    {
      GenderType genderType;

      if ((genderType = _context
        .GenderTypes
          .SingleOrDefault(g => g.Name ==
            GENDER_TYPE_NAME_MALE)) == null)
      {
        genderType = new GenderType();
        _context.Add(genderType);
      }
      genderType.Description = GENDER_TYPE_DESCRIPTION_MALE;
      genderType.IsActive = true;
      genderType.ModifiedAt = _now;
      genderType.ModifiedByUser = GetSystemAdminUser();
      genderType.Name = GENDER_TYPE_NAME_MALE;

      if ((genderType = _context
        .GenderTypes
          .SingleOrDefault(g => g.Name ==
            GENDER_TYPE_NAME_FEMALE)) == null)
      {
        genderType = new GenderType();
        _context.Add(genderType);
      }
      genderType.Description = GENDER_TYPE_DESCRIPTION_FEMALE;
      genderType.IsActive = true;
      genderType.ModifiedAt = _now;
      genderType.ModifiedByUser = GetSystemAdminUser();
      genderType.Name = GENDER_TYPE_NAME_FEMALE;

      if ((genderType = _context.GenderTypes
        .SingleOrDefault(g => g.Name ==
          GENDER_TYPE_NAME_OTHER)) == null)
      {
        genderType = new GenderType();
        _context.Add(genderType);
      }
      genderType.Description = GENDER_TYPE_DESCRIPTION_OTHER;
      genderType.IsActive = true;
      genderType.ModifiedAt = _now;
      genderType.ModifiedByUser = GetSystemAdminUser();
      genderType.Name = GENDER_TYPE_NAME_OTHER;
    }
  }
}