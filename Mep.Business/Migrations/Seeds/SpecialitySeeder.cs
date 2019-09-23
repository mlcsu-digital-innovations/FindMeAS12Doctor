using System.Linq;
using Mep.Data.Entities;

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
                    .SingleOrDefault(u => u.Name == "Speciality 1")) == null)
      {
        speciality = new Speciality();
        _context.Add(speciality);
      }
      speciality.Description = "Speciality 1 Test";
      speciality.IsActive = true;
      speciality.ModifiedAt = _now;
      speciality.ModifiedByUser = GetSystemAdminUser();
      speciality.Name = "Speciality 1";

      if ((speciality = 
            _context.Specialities                     
                    .SingleOrDefault(u => u.Name == "Speciality 2")) == null)
      {
        speciality = new Speciality();        
        _context.Add(speciality);
      }
      speciality.Description = "Speciality 2 Test";
      speciality.IsActive = true;
      speciality.ModifiedAt = _now;
      speciality.ModifiedByUser = GetSystemAdminUser();
      speciality.Name = "Speciality 2";      
    }
  }
}