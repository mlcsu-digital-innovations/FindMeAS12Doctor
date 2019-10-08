using Mep.Data.Entities;
using System;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class OnCallUsersSeeder : SeederBase
  {

    internal OnCallUsersSeeder(ApplicationContext context)
      : base(context)
    {
    }

    internal void SeedData()
    {
      OnCallUser onCallUser;

      if ((onCallUser = _context
        .OnCallUsers
          .SingleOrDefault(g => g.UserId ==
            GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_MALE)))
              == null)
      {
        onCallUser = new OnCallUser();
        _context.Add(onCallUser);
      }
      onCallUser.DateTimeEnd = _now.AddHours(1);
      onCallUser.DateTimeStart = _now;
      onCallUser.IsActive = true;
      onCallUser.ModifiedAt = _now;
      onCallUser.ModifiedByUser = GetSystemAdminUser();
      onCallUser.UserId =
        GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_MALE);

      if ((onCallUser = _context
        .OnCallUsers
          .SingleOrDefault(g => g.UserId ==
            GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_FEMALE)))
              == null)
      {
        onCallUser = new OnCallUser();
        _context.Add(onCallUser);
      }
      onCallUser.DateTimeEnd = _now.AddHours(1);
      onCallUser.DateTimeStart = _now;
      onCallUser.IsActive = true;
      onCallUser.ModifiedAt = _now;
      onCallUser.ModifiedByUser = GetSystemAdminUser();
      onCallUser.UserId =
        GetUserIdByDisplayname(USER_DISPLAY_NAME_DOCTOR_FEMALE);
    }
  }
}