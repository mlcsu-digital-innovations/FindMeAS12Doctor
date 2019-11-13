using Fmas12d.Data.Entities;
using System;
using System.Linq;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class OnCallUsersSeeder : SeederBase<OnCallUser>
  {
    #region 
    internal readonly DateTimeOffset END_DOCTOR_FEMALE = 
      new DateTimeOffset(2019, 10, 10, 17, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal readonly DateTimeOffset END_DOCTOR_MALE = 
      new DateTimeOffset(2019, 10, 10, 16, 00, 00, 00, DateTimeOffset.Now.Offset);      
    internal readonly DateTimeOffset START_DOCTOR_FEMALE =
      new DateTimeOffset(2019, 10, 10, 9, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal readonly DateTimeOffset START_DOCTOR_MALE =
      new DateTimeOffset(2019, 10, 10, 8, 00, 00, 00, DateTimeOffset.Now.Offset);          
    #endregion

    internal void SeedData()
    {
      AddOrUpdate(
        UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE,
        START_DOCTOR_FEMALE,
        END_DOCTOR_FEMALE
      );

      AddOrUpdate(
        UserSeeder.DISPLAY_NAME_DOCTOR_MALE,
        START_DOCTOR_MALE,
        END_DOCTOR_MALE
      );
    }

    private void AddOrUpdate(string name, DateTimeOffset start, DateTimeOffset end)
    {
      OnCallUser onCallUser;

      if ((onCallUser = Context.OnCallUsers
          .SingleOrDefault(g => g.UserId == GetUserByDisplayName(name).Id)) == null)
      {
        onCallUser = new OnCallUser();
        Context.Add(onCallUser);
      }
      onCallUser.DateTimeEnd = end;
      onCallUser.DateTimeStart = start;
      onCallUser.UserId = GetUserByDisplayName(name).Id;
      PopulateActiveAndModifiedWithSystemUser(onCallUser);
    }
  }
}