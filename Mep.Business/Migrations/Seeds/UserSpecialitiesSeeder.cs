using Mep.Data.Entities;
using System;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class UserSpecialitiesSeeder : SeederBase<UserSpeciality>
  {
    internal void SeedData()
    {
      AddOrUpdate(
        Models.Speciality.SECTION_12,
        UserSeeder.DISPLAY_NAME_DOCTOR_S12_APPROVED
      );
    }

    private void AddOrUpdate(int specialityId, string userName)
    {
      UserSpeciality userSpeciality;

      if ((userSpeciality = _context.UserSpecialities
        .Where(u => u.SpecialityId == specialityId)
        .SingleOrDefault(g => g.UserId == GetUserByDisplayName(userName).Id)) == null)
      {
        userSpeciality = new UserSpeciality();
        _context.Add(userSpeciality);
      }
      userSpeciality.SpecialityId = specialityId;
      userSpeciality.UserId = GetUserByDisplayName(userName).Id;      
      PopulateActiveAndModifiedWithSystemUser(userSpeciality);
    }
  }
}