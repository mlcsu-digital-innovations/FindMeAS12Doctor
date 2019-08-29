using System;
using System.Linq;
using Mep.Data.Entities;

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
      DateTimeOffset now = DateTimeOffset.Now;

      if ((genderType =
            _context.GenderTypes
                    .SingleOrDefault(g => g.Name == "Male")) == null)
      {
        genderType = new GenderType();
        _context.Add(genderType);
      }

      genderType.IsActive = true;
      genderType.ModifiedAt = now;
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
      genderType.ModifiedAt = now;
      genderType.ModifiedByUser = GetSystemAdminUser();
      genderType.Name = "Female";
      genderType.Description = "Female";
    }
  }
}