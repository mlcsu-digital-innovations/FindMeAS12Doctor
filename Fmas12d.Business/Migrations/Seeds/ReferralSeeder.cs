using Fmas12d.Business.Helpers;
using Fmas12d.Data.Entities;
using System;
using System.Collections.Generic;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class ReferralSeeder : SeederBase<Referral>
  {
    #region Constants
    internal const string ASSESSMENT_SCHEDULED_ADDRESS1 = "97 Thornton Rd";
    internal const string ASSESSMENT_SCHEDULED_ADDRESS2 = "Stoke-on-Trent";
    internal const string ASSESSMENT_SCHEDULED_ADDRESS3 = null;
    internal const string ASSESSMENT_SCHEDULED_ADDRESS4 = null;
    internal const decimal ASSESSMENT_SCHEDULED_LATITUDE = 53.008785m;
    internal const decimal ASSESSMENT_SCHEDULED_LONGITUDE = -2.178602m;
    internal const string ASSESSMENT_SCHEDULED_MEETING_ARRANGEMENT_COMMENT =
      "Assessment Scheduled Arrangement Comment";
    internal readonly DateTimeOffset ASSESSMENT_SCHEDULED_MUST_BE_COMPLETED_BY =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day,
                         17, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal const string ASSESSMENT_SCHEDULED_POSTCODE = "ST4 2BD";

    internal const string AWAITING_RESPONSES_ADDRESS1 = "Baldwins Gate Filling Station";
    internal const string AWAITING_RESPONSES_ADDRESS2 = "Newcastle Road";
    internal const string AWAITING_RESPONSES_ADDRESS3 = "Baldwin's Gate";
    internal const string AWAITING_RESPONSES_ADDRESS4 = "Newcastle";
    internal const decimal AWAITING_RESPONSES_LATITUDE = 52.958925m;
    internal const decimal AWAITING_RESPONSES_LONGITUDE = -2.307405m;
    internal const string AWAITING_RESPONSES_MEETING_ARRANGEMENT_COMMENT =
      "Allocating Meeting Arrangement Comment";
    internal readonly DateTimeOffset AWAITING_RESPONSES_MUST_BE_COMPLETED_BY =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day,
                         16, 30, 00, 00, DateTimeOffset.Now.Offset);
    internal const string AWAITING_RESPONSES_POSTCODE = "ST5 5DA";

    internal const string RESPONSES_PARTIAL_ADDRESS1 = "39 Leacroft Rd";
    internal const string RESPONSES_PARTIAL_ADDRESS2 = "Stoke-on-Trent";
    internal const string RESPONSES_PARTIAL_ADDRESS3 = null;
    internal const string RESPONSES_PARTIAL_ADDRESS4 = null;
    internal const decimal RESPONSES_PARTIAL_LATITUDE = 52.973132m;
    internal const decimal RESPONSES_PARTIAL_LONGITUDE = -2.104617m;
    internal const string RESPONSES_PARTIAL_MEETING_ARRANGEMENT_COMMENT =
      "Responses Partial Arrangement Comment";
    internal readonly DateTimeOffset RESPONSES_PARTIAL_MUST_BE_COMPLETED_BY =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day,
                         17, 30, 00, 00, DateTimeOffset.Now.Offset);
    internal const string RESPONSES_PARTIAL_POSTCODE = "ST3 7AL";

    internal const string SELECTING_DOCTORS_ADDRESS1 = "285 Clayton Rd";
    internal const string SELECTING_DOCTORS_ADDRESS2 = "Newcastle";
    internal const string SELECTING_DOCTORS_ADDRESS3 = null;
    internal const string SELECTING_DOCTORS_ADDRESS4 = null;
    internal const decimal SELECTING_DOCTORS_LATITUDE = 52.989739m;
    internal const decimal SELECTING_DOCTORS_LONGITUDE = -2.222786m;
    internal const string SELECTING_DOCTORS_MEETING_ARRANGEMENT_COMMENT =
      "Selecting Doctors Arrangement Comment";
    internal readonly DateTimeOffset SELECTING_DOCTORS_MUST_BE_COMPLETED_BY =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day,
                         15, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal const string SELECTING_DOCTORS_POSTCODE = "ST5 3EU";
    #endregion

    private readonly AssessmentDoctorSeeder _assessmentDoctorSeeder =
      new AssessmentDoctorSeeder();
    private readonly AssessmentSeeder _assessmentSeeder = new AssessmentSeeder();
    private readonly UserAssessmentNotificationSeeder _userAssessmentNotificationSeeder =
      new UserAssessmentNotificationSeeder();
    private readonly UserAssessmentClaimsSeeder _userAssessmentClaimsSeeder =
      new UserAssessmentClaimsSeeder();

    /// <summary>
    /// Deletes all the following seeds because updating is too difficult:
    /// UserAssessmentClaims
    /// UserAssessmentNotification
    /// Assessment Doctor
    /// Assessment Details
    /// Assessment
    /// Referral
    /// </summary>
    internal void SeedData()
    {

      _userAssessmentClaimsSeeder.DeleteSeeds();
      _userAssessmentNotificationSeeder.DeleteSeeds();
      _assessmentDoctorSeeder.DeleteSeeds();
      _assessmentSeeder.DeleteSeeds();
      DeleteSeeds();
      Context.SaveChanges();

      AddNewReferral();

      AddSelectingDoctorsReferral();

      AddAwaitingResponsesReferral();

      AddResponsesPartialReferral();

      AddAssessmentScheduledReferral();

      AddOpenReferralWithPreviousReferral();

      AddReferralsWithSuccessfulAssessment();
    }

    private void AddReferralsWithSuccessfulAssessment()
    {
      string[] ccgNames = new string[]
      {
        CcgSeeder.BLACKBURN_WITH_DARWEN,
        CcgSeeder.CANNOCK_CHASE,
        CcgSeeder.EAST_LANCASHIRE,
        CcgSeeder.EAST_LEICESTERSHIRE_AND_RUTLAND,
        CcgSeeder.EAST_STAFFORDSHIRE,
        CcgSeeder.LEICESTER_CITY,
        CcgSeeder.MORECAMBE_BAY,
        CcgSeeder.NORTH_STAFFORDSHIRE,
        CcgSeeder.SOUTH_EAST_STAFFORDSHIRE_AND_SEISDON_PENINSULA,
        CcgSeeder.STAFFORD_AND_SURROUNDS,
        CcgSeeder.STOKE_ON_TRENT,
        CcgSeeder.WEST_LEICESTERSHIRE
      };

      for (int i = 0; i < ccgNames.Length; i++)
      {
        AddReferralWithSuccessfulAssessment(ccgNames[i], DateTimeOffset.Now.AddDays(i));
      }

    }

    private void AddReferralWithSuccessfulAssessment(
      string ccgName,
      DateTimeOffset assessmentCompletedTime
    )
    {
      int amhpUserId = GetUserByDisplayName(UserSeeder.DISPLAY_NAME_AMHP_FEMALE).Id;

      List<AssessmentDoctor> assessmentDoctors = new List<AssessmentDoctor>
      {
        _assessmentDoctorSeeder.Create(
          attendanceConfirmedByUserId: amhpUserId,
          distance: Distance.CalculateDistanceAsCrowFlies(
            ASSESSMENT_SCHEDULED_LATITUDE,
            ASSESSMENT_SCHEDULED_LONGITUDE,
            ContactDetailsSeeder.LATITUDE_DOCTOR_FEMALE_BASE,
            ContactDetailsSeeder.LONGITUDE_DOCTOR_FEMALE_BASE
          ),
          doctorUserName: UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE,
          hasExcepted: true,
          latitude: ContactDetailsSeeder.LATITUDE_DOCTOR_FEMALE_BASE,
          longitude: ContactDetailsSeeder.LONGITUDE_DOCTOR_FEMALE_BASE,
          postcode: ContactDetailsSeeder.POSTCODE_DOCTOR_FEMALE_BASE,
          respondedAt: assessmentCompletedTime.AddHours(-2),
          statusId: Models.AssessmentDoctorStatus.ATTENDED
        )
      };

      List<Assessment> assessments = new List<Assessment> {
        _assessmentSeeder.Create(
          address1: ASSESSMENT_SCHEDULED_ADDRESS1,
          address2: ASSESSMENT_SCHEDULED_ADDRESS2,
          address3: ASSESSMENT_SCHEDULED_ADDRESS3,
          address4: ccgName,
          amhpUserName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE,
          ccgName: ccgName,
          createdByUserName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE,
          completedTime: assessmentCompletedTime,
          doctors: assessmentDoctors,
          meetingArrangementComment: ASSESSMENT_SCHEDULED_MEETING_ARRANGEMENT_COMMENT,
          mustBeCompletedBy: ASSESSMENT_SCHEDULED_MUST_BE_COMPLETED_BY,
          postcode: ASSESSMENT_SCHEDULED_POSTCODE,
          specialityId: Models.Speciality.LEARNING_DISABILITY,
          isSuccessful: true,
          scheduledTime: assessmentCompletedTime.AddHours(-1)
        )
      };

      AddReferral(
        alternativeIdentifier:
          PatientSeeder.ALTERNATIVE_IDENTIFIER_FOR_ASSESSMENT_SCHEDULED_REFERRAL,
        createdAt: assessmentCompletedTime.AddHours(-3),
        assessments: assessments,
        leadAmhpName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
        referralStatusId: Models.ReferralStatus.ASSESSMENT_SCHEDULED
      );
    }

    private void AddResponsesPartialReferral()
    {
      List<UserAssessmentNotification> userAssessmentNotifications =
        new List<UserAssessmentNotification>() {
        _userAssessmentNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          userName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE
        ),
        _userAssessmentNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          userName: UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE
        ),
        _userAssessmentNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          userName: UserSeeder.DISPLAY_NAME_DOCTOR_MALE
        )
      };

      List<AssessmentDoctor> assessmentDoctors = new List<AssessmentDoctor>
      {
        _assessmentDoctorSeeder.Create(
          distance: Distance.CalculateDistanceAsCrowFlies(
            RESPONSES_PARTIAL_LATITUDE,
            RESPONSES_PARTIAL_LONGITUDE,
            ContactDetailsSeeder.LATITUDE_DOCTOR_MALE_BASE,
            ContactDetailsSeeder.LONGITUDE_DOCTOR_MALE_BASE
          ),
          doctorUserName: UserSeeder.DISPLAY_NAME_DOCTOR_MALE,
          latitude: ContactDetailsSeeder.LATITUDE_DOCTOR_MALE_BASE,
          longitude: ContactDetailsSeeder.LONGITUDE_DOCTOR_MALE_BASE,
          postcode: ContactDetailsSeeder.POSTCODE_DOCTOR_MALE_BASE,
          statusId: Models.AssessmentDoctorStatus.SELECTED
        ),
        _assessmentDoctorSeeder.Create(
          distance: Distance.CalculateDistanceAsCrowFlies(
            RESPONSES_PARTIAL_LATITUDE,
            RESPONSES_PARTIAL_LONGITUDE,
            ContactDetailsSeeder.LATITUDE_DOCTOR_FEMALE_BASE,
            ContactDetailsSeeder.LONGITUDE_DOCTOR_FEMALE_BASE
          ),
          doctorUserName: UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE,
          latitude: ContactDetailsSeeder.LATITUDE_DOCTOR_FEMALE_BASE,
          longitude: ContactDetailsSeeder.LONGITUDE_DOCTOR_FEMALE_BASE,
          postcode: ContactDetailsSeeder.POSTCODE_DOCTOR_FEMALE_BASE,
          statusId: Models.AssessmentDoctorStatus.SELECTED
        )
      };

      List<Assessment> assessments = new List<Assessment> {
        _assessmentSeeder.Create(
          address1: RESPONSES_PARTIAL_ADDRESS1,
          address2: RESPONSES_PARTIAL_ADDRESS2,
          address3: RESPONSES_PARTIAL_ADDRESS3,
          address4: RESPONSES_PARTIAL_ADDRESS4,
          amhpUserName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE,
          ccgName: CcgSeeder.STOKE_ON_TRENT,
          createdByUserName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE,
          doctors: assessmentDoctors,
          meetingArrangementComment: RESPONSES_PARTIAL_MEETING_ARRANGEMENT_COMMENT,
          mustBeCompletedBy: RESPONSES_PARTIAL_MUST_BE_COMPLETED_BY,
          postcode: RESPONSES_PARTIAL_POSTCODE,
          specialityId: Models.Speciality.LEARNING_DISABILITY,
          userAssessmentNotifications: userAssessmentNotifications
        )
      };

      AddReferral(
        alternativeIdentifier:
          PatientSeeder.ALTERNATIVE_IDENTIFIER_FOR_RESPONSES_PARTIAL_REFERRAL,
        createdAt: _now,
        assessments: assessments,
        leadAmhpName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
        referralStatusId: Models.ReferralStatus.RESPONSES_PARTIAL
      );

    }

    private void AddAwaitingResponsesReferral()
    {
      List<UserAssessmentNotification> userAssessmentNotifications =
        new List<UserAssessmentNotification>() {
        _userAssessmentNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          userName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE
        ),
        _userAssessmentNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          userName: UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE
        ),
        _userAssessmentNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          userName: UserSeeder.DISPLAY_NAME_DOCTOR_MALE
        ),
      };

      List<AssessmentDoctor> assessmentDoctors = new List<AssessmentDoctor>
      {
        _assessmentDoctorSeeder.Create(
          distance: Distance.CalculateDistanceAsCrowFlies(
            AWAITING_RESPONSES_LATITUDE,
            AWAITING_RESPONSES_LONGITUDE,
            ContactDetailsSeeder.LATITUDE_DOCTOR_FEMALE_BASE,
            ContactDetailsSeeder.LONGITUDE_DOCTOR_FEMALE_BASE
          ),
          doctorUserName: UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE,
          latitude: ContactDetailsSeeder.LATITUDE_DOCTOR_FEMALE_BASE,
          longitude: ContactDetailsSeeder.LONGITUDE_DOCTOR_FEMALE_BASE,
          postcode: ContactDetailsSeeder.POSTCODE_DOCTOR_FEMALE_BASE,
          statusId: Models.AssessmentDoctorStatus.SELECTED
        ),
        _assessmentDoctorSeeder.Create(
          distance: Distance.CalculateDistanceAsCrowFlies(
            AWAITING_RESPONSES_LATITUDE,
            AWAITING_RESPONSES_LONGITUDE,
            ContactDetailsSeeder.LATITUDE_DOCTOR_MALE_BASE,
            ContactDetailsSeeder.LONGITUDE_DOCTOR_MALE_BASE
          ),
          doctorUserName: UserSeeder.DISPLAY_NAME_DOCTOR_MALE,
          latitude: ContactDetailsSeeder.LATITUDE_DOCTOR_MALE_BASE,
          longitude: ContactDetailsSeeder.LONGITUDE_DOCTOR_MALE_BASE,
          postcode: ContactDetailsSeeder.POSTCODE_DOCTOR_MALE_BASE,
          statusId: Models.AssessmentDoctorStatus.SELECTED
        )
      };

      List<Assessment> assessments = new List<Assessment> {
        _assessmentSeeder.Create(
          address1: AWAITING_RESPONSES_ADDRESS1,
          address2: AWAITING_RESPONSES_ADDRESS2,
          address3: AWAITING_RESPONSES_ADDRESS3,
          address4: AWAITING_RESPONSES_ADDRESS4,
          amhpUserName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE,
          ccgName: CcgSeeder.STOKE_ON_TRENT,
          createdByUserName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE,
          doctors: assessmentDoctors,
          meetingArrangementComment: AWAITING_RESPONSES_MEETING_ARRANGEMENT_COMMENT,
          mustBeCompletedBy: AWAITING_RESPONSES_MUST_BE_COMPLETED_BY,
          postcode: AWAITING_RESPONSES_POSTCODE,
          specialityId: Models.Speciality.CHILDREN,
          userAssessmentNotifications: userAssessmentNotifications
        )
      };

      AddReferral(
        alternativeIdentifier: PatientSeeder.ALTERNATIVE_IDENTIFIER_FOR_AWAITING_RESPONSES_REFERRAL,
        createdAt: _now,
        assessments: assessments,
        leadAmhpName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
        referralStatusId: Models.ReferralStatus.AWAITING_RESPONSES
      );
    }

    private void AddAssessmentScheduledReferral()
    {
      List<UserAssessmentNotification> userAssessmentNotifications =
        new List<UserAssessmentNotification>() {
        _userAssessmentNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          userName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE
        ),
        _userAssessmentNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          userName: UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE
        ),
        _userAssessmentNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          userName: UserSeeder.DISPLAY_NAME_DOCTOR_MALE
        ),
        _userAssessmentNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.ALLOCATED_TO_ASSESSMENT,
          userName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE
        ),
        _userAssessmentNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.ALLOCATED_TO_ASSESSMENT,
          userName: UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE
        )
      };

      List<AssessmentDoctor> assessmentDoctors = new List<AssessmentDoctor>
      {
        _assessmentDoctorSeeder.Create(
          distance: Distance.CalculateDistanceAsCrowFlies(
            ASSESSMENT_SCHEDULED_LATITUDE,
            ASSESSMENT_SCHEDULED_LONGITUDE,
            ContactDetailsSeeder.LATITUDE_DOCTOR_MALE_BASE,
            ContactDetailsSeeder.LONGITUDE_DOCTOR_MALE_BASE
          ),
          doctorUserName: UserSeeder.DISPLAY_NAME_DOCTOR_MALE,
          latitude: ContactDetailsSeeder.LATITUDE_DOCTOR_MALE_BASE,
          longitude: ContactDetailsSeeder.LONGITUDE_DOCTOR_MALE_BASE,
          postcode: ContactDetailsSeeder.POSTCODE_DOCTOR_MALE_BASE,
          statusId: Models.AssessmentDoctorStatus.SELECTED
        ),
        _assessmentDoctorSeeder.Create(
          distance: Distance.CalculateDistanceAsCrowFlies(
            ASSESSMENT_SCHEDULED_LATITUDE,
            ASSESSMENT_SCHEDULED_LONGITUDE,
            ContactDetailsSeeder.LATITUDE_DOCTOR_FEMALE_BASE,
            ContactDetailsSeeder.LONGITUDE_DOCTOR_FEMALE_BASE
          ),
          doctorUserName: UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE,
          latitude: ContactDetailsSeeder.LATITUDE_DOCTOR_FEMALE_BASE,
          longitude: ContactDetailsSeeder.LONGITUDE_DOCTOR_FEMALE_BASE,
          postcode: ContactDetailsSeeder.POSTCODE_DOCTOR_FEMALE_BASE,
          statusId: Models.AssessmentDoctorStatus.ALLOCATED
        )
      };

      List<Assessment> assessments = new List<Assessment> {
        _assessmentSeeder.Create(
          address1: ASSESSMENT_SCHEDULED_ADDRESS1,
          address2: ASSESSMENT_SCHEDULED_ADDRESS2,
          address3: ASSESSMENT_SCHEDULED_ADDRESS3,
          address4: ASSESSMENT_SCHEDULED_ADDRESS4,
          amhpUserName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE,
          ccgName: CcgSeeder.STOKE_ON_TRENT,
          createdByUserName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE,
          doctors: assessmentDoctors,
          meetingArrangementComment: ASSESSMENT_SCHEDULED_MEETING_ARRANGEMENT_COMMENT,
          mustBeCompletedBy: ASSESSMENT_SCHEDULED_MUST_BE_COMPLETED_BY,
          postcode: ASSESSMENT_SCHEDULED_POSTCODE,
          specialityId: Models.Speciality.LEARNING_DISABILITY,
          userAssessmentNotifications: userAssessmentNotifications
        )
      };

      AddReferral(
        alternativeIdentifier:
          PatientSeeder.ALTERNATIVE_IDENTIFIER_FOR_ASSESSMENT_SCHEDULED_REFERRAL,
        createdAt: _now,
        assessments: assessments,
        leadAmhpName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
        referralStatusId: Models.ReferralStatus.ASSESSMENT_SCHEDULED
      );
    }

    private void AddSelectingDoctorsReferral()
    {
      List<UserAssessmentNotification> userAssessmentNotifications =
        new List<UserAssessmentNotification>() {
        _userAssessmentNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          userName: UserSeeder.DISPLAY_NAME_AMHP_MALE
        )
      };
      userAssessmentNotifications.ForEach(userAssessmentNotification =>
        PopulateActiveAndModifiedWithSystemUser(userAssessmentNotification)
      );

      List<AssessmentDetail> assessmentDetails = new List<AssessmentDetail> {
        new AssessmentDetail() {
          AssessmentDetailTypeId = GetAssessmentDetailTypeByName(
              AssessmentDetailTypesSeeder.NAME_POLICE_PRESENT).Id,
          IsActive = true
        }
      };
      assessmentDetails.ForEach(assessmentDetail =>
        PopulateActiveAndModifiedWithSystemUser(assessmentDetail)
      );

      List<Assessment> assessments = new List<Assessment> {
        _assessmentSeeder.Create(
          address1: SELECTING_DOCTORS_ADDRESS1,
          address2: SELECTING_DOCTORS_ADDRESS2,
          address3: SELECTING_DOCTORS_ADDRESS3,
          address4: SELECTING_DOCTORS_ADDRESS4,
          amhpUserName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
          ccgName: CcgSeeder.STOKE_ON_TRENT,
          createdByUserName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
          details: assessmentDetails,
          meetingArrangementComment: SELECTING_DOCTORS_MEETING_ARRANGEMENT_COMMENT,
          mustBeCompletedBy: SELECTING_DOCTORS_MUST_BE_COMPLETED_BY,
          postcode: SELECTING_DOCTORS_POSTCODE,
          specialityId: Models.Speciality.LEARNING_DISABILITY,
          userAssessmentNotifications: userAssessmentNotifications
        )
      };

      AddReferral(
        alternativeIdentifier: PatientSeeder.ALTERNATIVE_IDENTIFIER_FOR_SELECTING_DOCTORS_REFERRAL,
        createdAt: _now,
        assessments: assessments,
        leadAmhpName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
        referralStatusId: Models.ReferralStatus.SELECTING_DOCTORS
      );

    }

    private void AddOpenReferralWithPreviousReferral()
    {
      List<UserAssessmentNotification> userAssessmentNotifications =
        new List<UserAssessmentNotification>() {
        _userAssessmentNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          userName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE
        ),
        _userAssessmentNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          userName: UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE
        ),
        _userAssessmentNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.ALLOCATED_TO_ASSESSMENT,
          userName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE
        ),
        _userAssessmentNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.ALLOCATED_TO_ASSESSMENT,
          userName: UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE
        )
      };

      List<AssessmentDoctor> assessmentDoctors = new List<AssessmentDoctor>
      {
        _assessmentDoctorSeeder.Create(
          distance: Distance.CalculateDistanceAsCrowFlies(
            ASSESSMENT_SCHEDULED_LATITUDE,
            ASSESSMENT_SCHEDULED_LONGITUDE,
            ContactDetailsSeeder.LATITUDE_DOCTOR_FEMALE_BASE,
            ContactDetailsSeeder.LONGITUDE_DOCTOR_FEMALE_BASE
          ),
          doctorUserName: UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE,
          latitude: ContactDetailsSeeder.LATITUDE_DOCTOR_FEMALE_BASE,
          longitude: ContactDetailsSeeder.LONGITUDE_DOCTOR_FEMALE_BASE,
          postcode: ContactDetailsSeeder.POSTCODE_DOCTOR_FEMALE_BASE,
          statusId: Models.AssessmentDoctorStatus.ATTENDED
        )
      };

      List<Assessment> assessments = new List<Assessment> {
        _assessmentSeeder.Create(
          address1: ASSESSMENT_SCHEDULED_ADDRESS1,
          address2: ASSESSMENT_SCHEDULED_ADDRESS2,
          address3: ASSESSMENT_SCHEDULED_ADDRESS3,
          address4: ASSESSMENT_SCHEDULED_ADDRESS4,
          amhpUserName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE,
          ccgName: CcgSeeder.STOKE_ON_TRENT,
          createdByUserName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE,
          completedTime: _now,
          completedByUserName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE,
          completionConfirmationByUserName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
          doctors: assessmentDoctors,
          isSuccessful: false,
          meetingArrangementComment: ASSESSMENT_SCHEDULED_MEETING_ARRANGEMENT_COMMENT,
          mustBeCompletedBy: ASSESSMENT_SCHEDULED_MUST_BE_COMPLETED_BY,
          postcode: ASSESSMENT_SCHEDULED_POSTCODE,
          specialityId: Models.Speciality.LEARNING_DISABILITY,
          userAssessmentNotifications: userAssessmentNotifications,
          unsuccessfulAssessmentTypeId: Models.UnsuccessfulAssessmentType.REFUSED_ENTRY
        )
      };

      AddReferral(
        assessments: assessments,
        createdAt: _now,
        leadAmhpName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
        nhsNumber: PatientSeeder.NHS_NUMBER_CCG_NORTH_STAFFORDSHIRE,
        referralStatusId: Models.ReferralStatus.OPEN
      );
    }

    private void AddNewReferral()
    {
      AddReferral(
        createdAt: _now,
        leadAmhpName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
        nhsNumber: PatientSeeder.NHS_NUMBER_CCG_NORTH_STAFFORDSHIRE,
        referralStatusId: Models.ReferralStatus.NEW
      );
    }

    private Referral AddReferral(
        DateTimeOffset createdAt,
        string leadAmhpName,
        int referralStatusId,
        string alternativeIdentifier = null,
        List<Assessment> assessments = null,
        bool isPlannedAssessment = false,
        long? nhsNumber = null
    )
    {

      Referral referral = new Referral();
      Context.Add(referral);

      if (nhsNumber.HasValue)
      {
        referral.PatientId = GetPatientIdByNhsNumber((long)nhsNumber);
      }
      else
      {
        referral.PatientId = GetPatientIdByAlternativeIdentifier(alternativeIdentifier);
      }

      referral.CreatedAt = createdAt;
      referral.CreatedByUser = GetSystemAdminUser();
      referral.Assessments = assessments;
      referral.IsPlannedAssessment = isPlannedAssessment;
      referral.LeadAmhpUserId = GetUserByDisplayName(leadAmhpName).Id;
      referral.ReferralStatusId = GetReferralStatus(referralStatusId).Id;
      PopulateActiveAndModifiedWithSystemUser(referral);

      return referral;
    }
  }
}