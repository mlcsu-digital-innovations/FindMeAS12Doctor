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
          .SingleOrDefault(g => g.Name == "Male")) == null)
      {
        genderType = new GenderType();
        _context.Add(genderType);
      }

      genderType.IsActive = true;
      genderType.ModifiedAt = _now;
      genderType.ModifiedByUser = GetSystemAdminUser();
      genderType.Name = "Male";
      genderType.Description = "Male";

      if ((genderType =
        _context.GenderTypes
          .SingleOrDefault(g => g.Name == "Female")) == null)
      {
        genderType = new GenderType();
        _context.Add(genderType);
      }

      genderType.IsActive = true;
      genderType.ModifiedAt = _now;
      genderType.ModifiedByUser = GetSystemAdminUser();
      genderType.Name = "Female";
      genderType.Description = "Female";

      if ((genderType =
        _context.GenderTypes
          .SingleOrDefault(g => g.Name == "Other")) == null)
      {
        genderType = new GenderType();
        _context.Add(genderType);
      }

      genderType.IsActive = true;
      genderType.ModifiedAt = _now;
      genderType.ModifiedByUser = GetSystemAdminUser();
      genderType.Name = "Other";
      genderType.Description = "Other";
    }
  }
}