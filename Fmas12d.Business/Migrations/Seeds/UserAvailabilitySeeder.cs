using Fmas12d.Data.Entities;
using System;
using System.Linq;

namespace Fmas12d.Business.Migrations.Seeds
{
  /// <summary>
  /// DOCTOR_FEMALE avaibility
  /// 06:00-08:00 09:00-11:00 13:00-14:00 15:00-18:00
  /// </summary>
  internal class UserAvailabilitySeeder : SeederBase<UserAvailability>
  {
    #region Constants
    internal readonly DateTimeOffset END_DOCTOR_FEMALE_1 =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day,
                         8, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal readonly DateTimeOffset END_DOCTOR_MALE_1 =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day,
                         11, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal readonly DateTimeOffset END_DOCTOR_MALE_2 =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day,
                         15, 00, 00, 00, DateTimeOffset.Now.Offset);                         
    internal readonly DateTimeOffset END_DOCTOR_MALE_3 =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day,
                         17, 00, 00, 00, DateTimeOffset.Now.Offset);                         
    internal const decimal LATITUDE_DOCTOR_FEMALE = 52.991581m;
    internal const decimal LATITUDE_DOCTOR_MALE_1 = 52.921459m;
    internal const decimal LATITUDE_DOCTOR_MALE_2 = 53.107297m;
    internal const decimal LATITUDE_DOCTOR_MALE_3 = 52.991581m;
    internal const decimal LONGITUDE_DOCTOR_FEMALE = -2.167857m;
    internal const decimal LONGITUDE_DOCTOR_MALE_1 = -1.476385m;
    internal const decimal LONGITUDE_DOCTOR_MALE_2 = -1.562336m;
    internal const decimal LONGITUDE_DOCTOR_MALE_3 = -2.167857m;  
    internal readonly DateTimeOffset START_DOCTOR_FEMALE =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 
                         4, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal readonly DateTimeOffset START_DOCTOR_MALE_1 =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 
                         9, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal readonly DateTimeOffset START_DOCTOR_MALE_2 =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 
                         13, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal readonly DateTimeOffset START_DOCTOR_MALE_3 =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 
                         15, 30, 00, 00, DateTimeOffset.Now.Offset);  
    #endregion

    internal void SeedData()
    {
      Add(
        END_DOCTOR_FEMALE_1,
        LATITUDE_DOCTOR_FEMALE,
        LONGITUDE_DOCTOR_FEMALE,
        START_DOCTOR_FEMALE,
        Models.UserAvailabilityStatus.AVAILABLE,
        UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE
      );

    Add(
        END_DOCTOR_MALE_1,
        LATITUDE_DOCTOR_MALE_1,
        LONGITUDE_DOCTOR_MALE_1,
        START_DOCTOR_MALE_1,
        Models.UserAvailabilityStatus.AVAILABLE,
        UserSeeder.DISPLAY_NAME_DOCTOR_ND11
      );

    Add(
        END_DOCTOR_MALE_2,
        LATITUDE_DOCTOR_MALE_2,
        LONGITUDE_DOCTOR_MALE_2,
        START_DOCTOR_MALE_2,
        Models.UserAvailabilityStatus.AVAILABLE,
        UserSeeder.DISPLAY_NAME_DOCTOR_ND11
      );

    Add(
        END_DOCTOR_MALE_3,
        LATITUDE_DOCTOR_MALE_3,
        LONGITUDE_DOCTOR_MALE_3,
        START_DOCTOR_MALE_3,
        Models.UserAvailabilityStatus.AVAILABLE,
        UserSeeder.DISPLAY_NAME_DOCTOR_ND11
      );      

  
    }

    private void Add(
      DateTimeOffset end,
      decimal latitude,
      decimal longitude,
      DateTimeOffset start,
      int statudId,
      string userDisplayName,
      int? contactDetailTypeId = null,
      string postcode = null
    )
    {
      UserAvailability doctorAvailability;
      if ((doctorAvailability = Context.UserAvailabilities
        .Where(d => d.End == end)        
        .Where(d => d.Start == start)
        .Where(d => d.UserId == GetUserByDisplayName(userDisplayName).Id)
        .SingleOrDefault()) == null)
      {
        doctorAvailability = new UserAvailability();
        Context.Add(doctorAvailability);
      }

      doctorAvailability.ContactDetailId = contactDetailTypeId;
      doctorAvailability.End = end;
      doctorAvailability.Latitude = latitude;
      doctorAvailability.Longitude = longitude;
      doctorAvailability.Postcode = postcode;
      doctorAvailability.Start = start;
      doctorAvailability.UserAvailabilityStatusId = statudId;
      doctorAvailability.UserId = GetUserByDisplayName(userDisplayName).Id;

      PopulateActiveAndModifiedWithSystemUser(doctorAvailability);      
    }
  }
}