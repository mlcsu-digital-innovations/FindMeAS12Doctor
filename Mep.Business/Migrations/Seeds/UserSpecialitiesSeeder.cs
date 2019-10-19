using Mep.Data.Entities;
using System;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class UserSpecialitiesSeeder : SeederBase<UserSpeciality>
  {
    internal void SeedData()
    {
      UserSpeciality userSpeciality;

      if ((userSpeciality = _context.UserSpecialities
        .SingleOrDefault(g => g.UserId == GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_MALE).Id)) == null)
      {
        userSpeciality = new UserSpeciality();
        _context.Add(userSpeciality);
      }
      userSpeciality.IsActive = true;
      userSpeciality.ModifiedAt = _now;
      userSpeciality.ModifiedByUser = GetSystemAdminUser();
      userSpeciality.SpecialityId = GetSpecialityId();
      userSpeciality.UserId = GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_MALE).Id;

      if ((userSpeciality = _context
        .UserSpecialities
          .SingleOrDefault(g => g.UserId ==
            GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE).Id)) == null)
      {
        userSpeciality = new UserSpeciality();
        _context.Add(userSpeciality);
      }
      userSpeciality.IsActive = true;
      userSpeciality.ModifiedAt = _now;
      userSpeciality.ModifiedByUser = GetSystemAdminUser();
      userSpeciality.SpecialityId = GetSpecialityId();
      userSpeciality.UserId =
        GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE).Id;
    }
  }
}