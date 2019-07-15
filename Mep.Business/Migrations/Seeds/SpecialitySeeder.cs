using System;
using System.Linq;
using Mep.Data.Entities;

namespace Mep.Business.Migrations.Seeds
{
    public class SpecialitySeeder : SeederBase, ISeeder
    {
        public SpecialitySeeder(ApplicationContext context) 
            : base(context)
        {         
        }

        public void SeedData()
        {
            Speciality speciality;
            DateTimeOffset now = DateTimeOffset.Now;

            if ((speciality = _context.Specialities                                       
                                      .SingleOrDefault(u => u.Name == "Speciality 1")) == null)
            {
                speciality = new Speciality();                
                _context.Add(speciality);
            }
            speciality.Description = "Speciality 1 Test";
            speciality.IsActive = true;
            speciality.ModifiedAt = now;
            speciality.ModifiedByUserId = 1;            
            speciality.Name = "Speciality 1";

            if ((speciality = _context.Specialities                                       
                                      .SingleOrDefault(u => u.Name == "Speciality 2")) == null)
            {
                speciality = new Speciality();                
                _context.Add(speciality);
            }
            speciality.Description = "Speciality 2 Test";
            speciality.IsActive = true;
            speciality.ModifiedAt = now;
            speciality.ModifiedByUserId = 1;
            speciality.Name = "Speciality 2";
            
            _context.SaveChanges();
        }
    }
}