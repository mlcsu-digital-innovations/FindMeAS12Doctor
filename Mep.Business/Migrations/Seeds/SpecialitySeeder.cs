using Mep.Data.Entities;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class SpecialitySeeder : SeederBase
  {
    internal SpecialitySeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      Speciality speciality;

      if ((speciality =
        _context.Specialities
          .SingleOrDefault(u => u.Name == SPECIALITY_SECTION_12)) == null)
      {
        speciality = new Speciality();
        _context.Add(speciality);
      }
      speciality.Description = SPECIALITY_SECTION_12;
      speciality.IsActive = true;
      speciality.ModifiedAt = _now;
      speciality.ModifiedByUser = GetSystemAdminUser();
      speciality.Name = SPECIALITY_SECTION_12;
    }
  }
}