using Mep.Data.Entities;
using System.Linq;
using System;

namespace Mep.Business.Migrations.Seeds
{
  internal class UserSpecialitiesSeeder : SeederBase
  {

    internal UserSpecialitiesSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      UserSpeciality userSpeciality;

      if ((userSpeciality = _context
        .UserSpecialities
          .SingleOrDefault(g => g.UserId ==
            GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_MALE))) == null)
      {
        userSpeciality = new UserSpeciality();
        _context.Add(userSpeciality);
      }
      userSpeciality.IsActive = true;
      userSpeciality.ModifiedAt = _now;
      userSpeciality.ModifiedByUser = GetSystemAdminUser();
      userSpeciality.SpecialityId = GetSpecialityId();
      userSpeciality.UserId =
        GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_MALE);

      if ((userSpeciality = _context
        .UserSpecialities
          .SingleOrDefault(g => g.UserId ==
            GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_FEMALE))) == null)
      {
        userSpeciality = new UserSpeciality();
        _context.Add(userSpeciality);
      }
      userSpeciality.IsActive = true;
      userSpeciality.ModifiedAt = _now;
      userSpeciality.ModifiedByUser = GetSystemAdminUser();
      userSpeciality.SpecialityId = GetSpecialityId();
      userSpeciality.UserId =
        GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_FEMALE);
    }
  }
}