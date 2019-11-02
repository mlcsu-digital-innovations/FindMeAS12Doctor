using Fmas12d.Data.Entities;
using System;
using System.Collections.Generic;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class ReferralSeeder : SeederBase<Referral>
  {
    #region Constants
    internal const string AWAITING_RESPONSES_ADDRESS1 = "Baldwins Gate Filling Station";
    internal const string AWAITING_RESPONSES_ADDRESS2 = "Newcastle Road";
    internal const string AWAITING_RESPONSES_ADDRESS3 = "Baldwin's Gate";
    internal const string AWAITING_RESPONSES_ADDRESS4 = "Newcastle";
    internal const string AWAITING_RESPONSES_MEETING_ARRANGEMENT_COMMENT =
      "Allocating Meeting Arangement Comment";
    internal readonly DateTimeOffset AWAITING_RESPONSES_MUST_BE_COMPLETED_BY =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day,
                         16, 30, 00, 00, DateTimeOffset.Now.Offset);
    internal const string AWAITING_RESPONSES_POSTCODE = "ST5 5DA";

    internal const string ALLOCATED_DOCTORS_ADDRESS1 = "97 Thornton Rd";
    internal const string ALLOCATED_DOCTORS_ADDRESS2 = "Stoke-on-Trent";
    internal const string ALLOCATED_DOCTORS_ADDRESS3 = null;
    internal const string ALLOCATED_DOCTORS_ADDRESS4 = null;
    internal const string ALLOCATED_DOCTORS_MEETING_ARRGANEMENT_COMMENT =
      "Allocated Meeting Arangement Comment";
    internal readonly DateTimeOffset ALLOCATED_DOCTORS_MUST_BE_COMPLETED_BY =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day,
                         17, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal const string ALLOCATED_DOCTORS_POSTCODE = "ST4 2BD";    

    internal const string ASSIGNING_DOCTORS_ADDRESS1 = "285 Clayton Rd";
    internal const string ASSIGNING_DOCTORS_ADDRESS2 = "Newcastle";
    internal const string ASSIGNING_DOCTORS_ADDRESS3 = null;
    internal const string ASSIGNING_DOCTORS_ADDRESS4 = null;
    internal const string ASSIGNING_DOCTORS_MEETING_ARRANGEMENT_COMMENT =
      "Assigning Meeting Arangement Comment";
    internal readonly DateTimeOffset ASSIGNING_DOCTORS_MUST_BE_COMPLETED_BY =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day,
                         15, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal const string ASSIGNING_DOCTORS_POSTCODE = "ST5 3EU";    
    #endregion

    private readonly AssessmentDoctorSeeder _assessmentDoctorSeeder = 
      new AssessmentDoctorSeeder();
    private readonly AssessmentSeeder _assessmentSeeder = new AssessmentSeeder();
    private readonly UserAssessmentNotificationSeeder _userAssessmentNotificationSeeder =
      new UserAssessmentNotificationSeeder();

    /// <summary>
    /// Deletes all the following seeds because updating is too difficult:
    /// UserAssessmentNotification
    /// Assessment Doctor
    /// Assessment Details
    /// Assessment
    /// Referral
    /// </summary>
    internal void SeedData()
    {

      _userAssessmentNotificationSeeder.DeleteSeeds();
      _assessmentDoctorSeeder.DeleteSeeds();
      _assessmentSeeder.DeleteSeeds();
      DeleteSeeds();
      Context.SaveChanges();

      AddNewReferral();      

      AddSelectingDoctorsReferral();

      AddAwaitingResponsesReferral();

      AddAssessmentScheduledReferral();
    }

    private void AddAwaitingResponsesReferral()
    {
      List<UserAssessmentNotification> userAssessmentNotifications =
        new List<UserAssessmentNotification>() {
        _userAssessmentNotificationSeeder.Create(
          hasAccepted: true,
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          respondedAt: _now,
          userName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE
        ),
        _userAssessmentNotificationSeeder.Create(
          hasAccepted: true,
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          respondedAt: _now,
          userName: UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE
        ),
        _userAssessmentNotificationSeeder.Create(
          hasAccepted: true,
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          respondedAt: _now,
          userName: UserSeeder.DISPLAY_NAME_DOCTOR_MALE
        ),
      };

      List<AssessmentDoctor> assessmentDoctors = new List<AssessmentDoctor>
      {      
        _assessmentDoctorSeeder.Create(
          doctorUserName: UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE,
          statusId: Models.AssessmentDoctorStatus.SELECTED
        ),
        _assessmentDoctorSeeder.Create(
          doctorUserName: UserSeeder.DISPLAY_NAME_DOCTOR_MALE,
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
          specialityId: Models.Speciality.CHILD,
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
          hasAccepted: true,
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          respondedAt: _now,
          userName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE
        ),
        _userAssessmentNotificationSeeder.Create(
          hasAccepted: true,
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          respondedAt: _now,
          userName: UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE
        ),
        _userAssessmentNotificationSeeder.Create(
          hasAccepted: true,
          notificationTextId: Models.NotificationText.SELECTED_FOR_ASSESSMENT,
          respondedAt: _now,
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
          doctorUserName: UserSeeder.DISPLAY_NAME_DOCTOR_MALE,
          statusId: Models.AssessmentDoctorStatus.SELECTED
        ),        
        _assessmentDoctorSeeder.Create(
          doctorUserName: UserSeeder.DISPLAY_NAME_DOCTOR_FEMALE,
          statusId: Models.AssessmentDoctorStatus.ALLOCATED
        )
      };

      List<Assessment> assessments = new List<Assessment> {
        _assessmentSeeder.Create(
          address1: ALLOCATED_DOCTORS_ADDRESS1,
          address2: ALLOCATED_DOCTORS_ADDRESS2,
          address3: ALLOCATED_DOCTORS_ADDRESS3,
          address4: ALLOCATED_DOCTORS_ADDRESS4,
          amhpUserName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE,
          ccgName: CcgSeeder.STOKE_ON_TRENT,
          createdByUserName: UserSeeder.DISPLAY_NAME_AMHP_FEMALE,
          doctors: assessmentDoctors,
          meetingArrangementComment: ALLOCATED_DOCTORS_MEETING_ARRGANEMENT_COMMENT,
          mustBeCompletedBy: ALLOCATED_DOCTORS_MUST_BE_COMPLETED_BY,
          postcode: ALLOCATED_DOCTORS_POSTCODE,
          specialityId: Models.Speciality.LEARNING_DIFFICULTY,
          userAssessmentNotifications: userAssessmentNotifications
        )
      };

      AddReferral(
        alternativeIdentifier: PatientSeeder.ALTERNATIVE_IDENTIFIER_FOR_ALLOCATED_DOCTORS_REFERRAL,
        createdAt: _now,
        assessments: assessments,
        leadAmhpName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
        referralStatusId: Models.ReferralStatus.RESPONSES_PARTIAL
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
              AssessmentDetailTypesSeeder.NAME_AGRESSIVE_NEIGHBOUR).Id,
          IsActive = true          
        }
      };
      assessmentDetails.ForEach(assessmentDetail => 
        PopulateActiveAndModifiedWithSystemUser(assessmentDetail)
      );

      List<Assessment> assessments = new List<Assessment> {
        _assessmentSeeder.Create(
          address1: ASSIGNING_DOCTORS_ADDRESS1,
          address2: ASSIGNING_DOCTORS_ADDRESS2,
          address3: ASSIGNING_DOCTORS_ADDRESS3,
          address4: ASSIGNING_DOCTORS_ADDRESS4,
          amhpUserName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
          ccgName: CcgSeeder.STOKE_ON_TRENT,
          createdByUserName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
          details: assessmentDetails,
          meetingArrangementComment: ASSIGNING_DOCTORS_MEETING_ARRANGEMENT_COMMENT,
          mustBeCompletedBy: ASSIGNING_DOCTORS_MUST_BE_COMPLETED_BY,
          postcode: ASSIGNING_DOCTORS_POSTCODE,
          specialityId: Models.Speciality.LEARNING_DIFFICULTY,
          userAssessmentNotifications: userAssessmentNotifications
        )
      };

      AddReferral(
        alternativeIdentifier: PatientSeeder.ALTERNATIVE_IDENTIFIER_FOR_ASSIGNING_DOCTORS_REFERRAL,
        createdAt: _now,
        assessments: assessments,
        leadAmhpName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
        referralStatusId: Models.ReferralStatus.SELECTING_DOCTORS
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