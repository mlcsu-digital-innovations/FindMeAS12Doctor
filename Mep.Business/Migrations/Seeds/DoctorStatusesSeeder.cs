using Mep.Data.Entities;
using System;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  /// <summary>
  /// DOCTOR_FEMALE avaibility
  /// 06:00-08:00 09:00-11:00 13:00-14:00 15:00-18:00
  /// </summary>
  internal class DoctorStatusesSeeder : SeederBase<DoctorStatus>
  {
    #region Constants

    internal readonly DateTimeOffset AVAILABILITY_END_DOCTOR_FEMALE = 
      new DateTimeOffset(2019, 10, 10, 8, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal readonly DateTimeOffset AVAILABILITY_START_DOCTOR_FEMALE =
      new DateTimeOffset(2019, 10, 10, 6, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal readonly DateTimeOffset EXTENDED_AVAILABILITY_END1_DOCTOR_FEMALE =
      new DateTimeOffset(2019, 10, 10, 11, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal readonly DateTimeOffset EXTENDED_AVAILABILITY_END2_DOCTOR_FEMALE =
      new DateTimeOffset(2019, 10, 10, 14, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal readonly DateTimeOffset EXTENDED_AVAILABILITY_END3_DOCTOR_FEMALE =
      new DateTimeOffset(2019, 10, 10, 18, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal const decimal EXTENDED_AVAILABILITY_LATITUDE1_DOCTOR_FEMALE = 52.921459m;
    internal const decimal EXTENDED_AVAILABILITY_LATITUDE2_DOCTOR_FEMALE = 53.107297m;
    internal const decimal EXTENDED_AVAILABILITY_LATITUDE3_DOCTOR_FEMALE = 52.991581m;
    internal const decimal EXTENDED_AVAILABILITY_LONGITUDE1_DOCTOR_FEMALE = -1.476385m;
    internal const decimal EXTENDED_AVAILABILITY_LONGITUDE2_DOCTOR_FEMALE =  -1.562336m;
    internal const decimal EXTENDED_AVAILABILITY_LONGITUDE3_DOCTOR_FEMALE = -2.167857m;
    internal readonly DateTimeOffset EXTENDED_AVAILABILITY_START1_DOCTOR_FEMALE =
      new DateTimeOffset(2019, 10, 10, 9, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal readonly DateTimeOffset EXTENDED_AVAILABILITY_START2_DOCTOR_FEMALE =
      new DateTimeOffset(2019, 10, 10, 13, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal readonly DateTimeOffset EXTENDED_AVAILABILITY_START3_DOCTOR_FEMALE =
      new DateTimeOffset(2019, 10, 10, 15, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal const decimal LATITUDE_DOCTOR_FEMALE = 52.991581m;
    internal const decimal LONGITUDE_DOCTOR_FEMALE = -2.167857m;
    #endregion

    internal void SeedData()
    {
      DoctorStatus doctorStatus;

      if ((doctorStatus = _context.DoctorStatuses
        .SingleOrDefault(g => g.UserId == GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE).Id)) == null)
      {
        doctorStatus = new DoctorStatus();
        _context.Add(doctorStatus);
      }

      doctorStatus.AvailabilityEnd = AVAILABILITY_END_DOCTOR_FEMALE;
      doctorStatus.AvailabilityStart = AVAILABILITY_START_DOCTOR_FEMALE;

      doctorStatus.ExtendedAvailabilityEnd1 =
        EXTENDED_AVAILABILITY_END1_DOCTOR_FEMALE;
      doctorStatus.ExtendedAvailabilityEnd2 =
        EXTENDED_AVAILABILITY_END2_DOCTOR_FEMALE;
      doctorStatus.ExtendedAvailabilityEnd3 =
        EXTENDED_AVAILABILITY_END3_DOCTOR_FEMALE;

      doctorStatus.ExtendedAvailabilityLatitude1 =
        EXTENDED_AVAILABILITY_LATITUDE1_DOCTOR_FEMALE;
      doctorStatus.ExtendedAvailabilityLatitude2 =
        EXTENDED_AVAILABILITY_LATITUDE2_DOCTOR_FEMALE;
      doctorStatus.ExtendedAvailabilityLatitude3 =
        EXTENDED_AVAILABILITY_LATITUDE3_DOCTOR_FEMALE;

      doctorStatus.ExtendedAvailabilityLongitude1 =
        EXTENDED_AVAILABILITY_LONGITUDE1_DOCTOR_FEMALE;
      doctorStatus.ExtendedAvailabilityLongitude2 =
        EXTENDED_AVAILABILITY_LONGITUDE2_DOCTOR_FEMALE;
      doctorStatus.ExtendedAvailabilityLongitude3 =
        EXTENDED_AVAILABILITY_LONGITUDE3_DOCTOR_FEMALE;

      doctorStatus.ExtendedAvailabilityEnd1 =
        EXTENDED_AVAILABILITY_START1_DOCTOR_FEMALE;
      doctorStatus.ExtendedAvailabilityEnd2 =
        EXTENDED_AVAILABILITY_START2_DOCTOR_FEMALE;
      doctorStatus.ExtendedAvailabilityEnd3 =
        EXTENDED_AVAILABILITY_START3_DOCTOR_FEMALE;

      doctorStatus.Latitude = LATITUDE_DOCTOR_FEMALE;
      doctorStatus.Longitude = LONGITUDE_DOCTOR_FEMALE;
      doctorStatus.UserId = GetUserByDisplayName(USER_DISPLAY_NAME_DOCTOR_FEMALE).Id;

      PopulateActiveAndModifiedWithSystemUser(doctorStatus);
    }
  }
}