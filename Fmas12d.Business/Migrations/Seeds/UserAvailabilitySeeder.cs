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
          UserSeeder.DISPLAY_NAME_DOCTOR_MALE
        );

      Add(
          END_DOCTOR_MALE_2,
          LATITUDE_DOCTOR_MALE_2,
          LONGITUDE_DOCTOR_MALE_2,
          START_DOCTOR_MALE_2,
          Models.UserAvailabilityStatus.AVAILABLE,
          UserSeeder.DISPLAY_NAME_DOCTOR_MALE
        );

      Add(
          END_DOCTOR_MALE_3,
          LATITUDE_DOCTOR_MALE_3,
          LONGITUDE_DOCTOR_MALE_3,
          START_DOCTOR_MALE_3,
          Models.UserAvailabilityStatus.AVAILABLE,
          UserSeeder.DISPLAY_NAME_DOCTOR_MALE
        );


    }

    internal void SeedDataUat()
    {
      const int MAX_WEEKS = 18;

      DateTimeOffset TwoToEightStart =
        new DateTimeOffset(2020, 1, 6, 2, 0, 0, DateTimeOffset.Now.Offset);
      DateTimeOffset TwoToEightEnd =
        new DateTimeOffset(2020, 1, 6, 8, 0, 0, DateTimeOffset.Now.Offset);

      DateTimeOffset EightToFourteenStart =
        new DateTimeOffset(2020, 1, 6, 8, 0, 0, DateTimeOffset.Now.Offset);
      DateTimeOffset EightToFourteenEnd =
        new DateTimeOffset(2020, 1, 6, 14, 0, 0, DateTimeOffset.Now.Offset);

      DateTimeOffset FourteenToTwentyStart =
        new DateTimeOffset(2020, 1, 6, 14, 0, 0, DateTimeOffset.Now.Offset);
      DateTimeOffset FourteenToTwentyEnd =
        new DateTimeOffset(2020, 1, 6, 20, 0, 0, DateTimeOffset.Now.Offset);

      DateTimeOffset TwentyToTwoStart =
        new DateTimeOffset(2020, 1, 6, 20, 0, 0, DateTimeOffset.Now.Offset);
      DateTimeOffset TwentyToTwoEnd =
        new DateTimeOffset(2020, 1, 7, 2, 0, 0, DateTimeOffset.Now.Offset);


      for (int i = 0; i < MAX_WEEKS; i+= 2)
      {
        for (int j = 0; j < 7; j++)
        {
          AddUatAvailability(TwentyToTwoStart, TwentyToTwoEnd, UserSeeder.DISPLAY_NAME_DR_SNOW);
          AddUatAvailability(TwoToEightStart, TwoToEightEnd, UserSeeder.DISPLAY_NAME_DR_BELL);
          AddUatAvailability(EightToFourteenStart, EightToFourteenEnd, UserSeeder.DISPLAY_NAME_DR_WHITE);
          AddUatAvailability(FourteenToTwentyStart, FourteenToTwentyEnd, UserSeeder.DISPLAY_NAME_DR_BLACK);
          AddUatAvailability(TwentyToTwoStart, TwentyToTwoEnd, UserSeeder.DISPLAY_NAME_DR_BROWN);
          AddUatAvailability(TwoToEightStart, TwoToEightEnd, UserSeeder.DISPLAY_NAME_DR_JONES);
          AddUatAvailability(EightToFourteenStart, EightToFourteenEnd, UserSeeder.DISPLAY_NAME_DR_SMITH);
          AddUatAvailability(FourteenToTwentyStart, FourteenToTwentyEnd, UserSeeder.DISPLAY_NAME_DR_MORRIS);
          AddUatAvailability(TwentyToTwoStart, TwentyToTwoEnd, UserSeeder.DISPLAY_NAME_DR_BAILEY);
          AddUatAvailability(TwoToEightStart, TwoToEightEnd, UserSeeder.DISPLAY_NAME_DR_TAYLOR);

          TwoToEightStart = TwoToEightStart.AddDays(1);
          TwoToEightEnd = TwoToEightEnd.AddDays(1);
          EightToFourteenStart = EightToFourteenStart.AddDays(1);
          EightToFourteenEnd = EightToFourteenEnd.AddDays(1);
          FourteenToTwentyStart = FourteenToTwentyStart.AddDays(1);
          FourteenToTwentyEnd = FourteenToTwentyEnd.AddDays(1);
          TwentyToTwoStart = TwentyToTwoStart.AddDays(1);
          TwentyToTwoEnd = TwentyToTwoEnd.AddDays(1);          
        }

        for (int j = 0; j < 7; j++)
        {
          AddUatAvailability(EightToFourteenStart, EightToFourteenEnd, UserSeeder.DISPLAY_NAME_DR_SNOW);
          AddUatAvailability(FourteenToTwentyStart, FourteenToTwentyEnd, UserSeeder.DISPLAY_NAME_DR_BELL);
          AddUatAvailability(TwentyToTwoStart, TwentyToTwoEnd, UserSeeder.DISPLAY_NAME_DR_WHITE);
          AddUatAvailability(TwoToEightStart, TwoToEightEnd, UserSeeder.DISPLAY_NAME_DR_BLACK);
          AddUatAvailability(EightToFourteenStart, EightToFourteenEnd, UserSeeder.DISPLAY_NAME_DR_BROWN);
          AddUatAvailability(FourteenToTwentyStart, FourteenToTwentyEnd, UserSeeder.DISPLAY_NAME_DR_JONES);
          AddUatAvailability(TwentyToTwoStart, TwentyToTwoEnd, UserSeeder.DISPLAY_NAME_DR_SMITH);
          AddUatAvailability(TwoToEightStart, TwoToEightEnd, UserSeeder.DISPLAY_NAME_DR_MORRIS);
          AddUatAvailability(EightToFourteenStart, EightToFourteenEnd, UserSeeder.DISPLAY_NAME_DR_BAILEY);
          AddUatAvailability(FourteenToTwentyStart, FourteenToTwentyEnd, UserSeeder.DISPLAY_NAME_DR_TAYLOR);

          TwoToEightStart = TwoToEightStart.AddDays(1);
          TwoToEightEnd = TwoToEightEnd.AddDays(1);
          EightToFourteenStart = EightToFourteenStart.AddDays(1);
          EightToFourteenEnd = EightToFourteenEnd.AddDays(1);
          FourteenToTwentyStart = FourteenToTwentyStart.AddDays(1);
          FourteenToTwentyEnd = FourteenToTwentyEnd.AddDays(1);
          TwentyToTwoStart = TwentyToTwoStart.AddDays(1);
          TwentyToTwoEnd = TwentyToTwoEnd.AddDays(1);       
        }
      }
    }

    private void AddUatAvailability(
      DateTimeOffset start,
      DateTimeOffset end,
      string userDisplayName
    )
    {
      ContactDetail contactDetail = Context.ContactDetails
        .Where(cd => cd.ContactDetailTypeId == Models.ContactDetailType.BASE)
        .Single(cd => cd.UserId == GetUserByDisplayName(userDisplayName).Id);

      Add(
        end,
        contactDetail.Latitude,
        contactDetail.Longitude,
        start,
        Models.UserAvailabilityStatus.AVAILABLE,
        userDisplayName,
        contactDetail.Id
      );
    }

    private void Add(
      DateTimeOffset end,
      decimal latitude,
      decimal longitude,
      DateTimeOffset start,
      int statusId,
      string userDisplayName,
      int? contactDetailId = null,
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

      doctorAvailability.ContactDetailId = contactDetailId;
      doctorAvailability.End = end;
      doctorAvailability.Latitude = latitude;
      doctorAvailability.Longitude = longitude;
      doctorAvailability.Postcode = postcode;
      doctorAvailability.Start = start;
      doctorAvailability.UserAvailabilityStatusId = statusId;
      doctorAvailability.UserId = GetUserByDisplayName(userDisplayName).Id;

      PopulateActiveAndModifiedWithSystemUser(doctorAvailability);
    }
  }
}