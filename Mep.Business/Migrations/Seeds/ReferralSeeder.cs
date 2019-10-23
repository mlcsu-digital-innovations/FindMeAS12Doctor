using Mep.Data.Entities;
using System;
using System.Collections.Generic;

namespace Mep.Business.Migrations.Seeds
{
  internal class ReferralSeeder : SeederBase<Referral>
  {
    #region Constants
    internal const string ASSIGNING_DOCTORS_ADDRESS1 = "Baldwins Gate Filling Station";
    internal const string ASSIGNING_DOCTORS_ADDRESS2 = "Newcastle Road";
    internal const string ASSIGNING_DOCTORS_ADDRESS3 = "Baldwin's Gate";
    internal const string ASSIGNING_DOCTORS_ADDRESS4 = "Newcastle";
    internal const string ASSIGNING_DOCTORS_MEETING_ARRGANEMENT_COMMENT =
      "Allocating Meeting Arangement Comment";
    internal readonly DateTimeOffset ASSIGNING_DOCTORS_MUST_BE_COMPLETED_BY =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day,
                         15, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal const string ASSIGNING_DOCTORS_POSTCODE = "ST5 5DA";
    #endregion

    private readonly ExaminationSeeder _examinationSeeder = new ExaminationSeeder();
    private readonly UserExaminationNotificationSeeder _userExaminationNotificationSeeder =
      new UserExaminationNotificationSeeder();

    /// <summary>
    /// Deletes all the following seeds because updating is too difficult:
    /// UserExaminationNotification
    /// Examination Details
    /// Examination
    /// Referral
    /// </summary>
    internal void SeedData()
    {

      _userExaminationNotificationSeeder.DeleteSeeds();
      _examinationSeeder.DeleteSeeds();
      DeleteSeeds();
      Context.SaveChanges();

      AddNewReferral();

      AddAssigningDoctorsReferral();

    }

    private void AddAssigningDoctorsReferral()
    {
      List<UserExaminationNotification> userExaminationNotifications =
        new List<UserExaminationNotification>() {
        _userExaminationNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.ASSIGNED_TO_EXAMINATION,
          userName: UserSeeder.DISPLAY_NAME_AMHP_MALE
        )
      };
      userExaminationNotifications.ForEach(userExaminationNotification => 
        PopulateActiveAndModifiedWithSystemUser(userExaminationNotification)
      );

      List<ExaminationDetail> examinationDetails = new List<ExaminationDetail> {
        new ExaminationDetail() {
          ExaminationDetailTypeId = GetExaminationDetailTypeByName(
              ExaminationDetailTypeSeeder.NAME_AGRESSIVE_NEIGHBOUR).Id,
          IsActive = true          
        }
      };
      examinationDetails.ForEach(examinationDetail => 
        PopulateActiveAndModifiedWithSystemUser(examinationDetail)
      );

      List<Examination> examinations = new List<Examination> {
        _examinationSeeder.Create(
          address1: ASSIGNING_DOCTORS_ADDRESS1,
          address2: ASSIGNING_DOCTORS_ADDRESS2,
          address3: ASSIGNING_DOCTORS_ADDRESS3,
          address4: ASSIGNING_DOCTORS_ADDRESS4,
          ccgName: CcgSeeder.STOKE_ON_TRENT,
          createdByUserName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
          details: examinationDetails,
          meetingArrangementComment: ASSIGNING_DOCTORS_MEETING_ARRGANEMENT_COMMENT,
          mustBeCompletedBy: ASSIGNING_DOCTORS_MUST_BE_COMPLETED_BY,
          postcode: ASSIGNING_DOCTORS_POSTCODE,
          userExaminationNotifications: userExaminationNotifications
        )
      };

      AddReferral(
        alternativeIdentifier: PatientSeeder.ALTERNATIVE_IDENTIFIER_CCG_STOKE_ON_TRENT,
        createdAt: _now,
        examinations: examinations,
        leadAmhpName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
        referralStatusId: Models.ReferralStatus.ASSIGNING_DOCTORS
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
        List<Examination> examinations = null,
        bool isPlannedExamination = false,
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
      referral.Examinations = examinations;
      referral.IsPlannedExamination = isPlannedExamination;
      referral.LeadAmhpUserId = GetUserByDisplayName(leadAmhpName).Id;
      referral.ReferralStatusId = GetReferralStatus(referralStatusId).Id;
      PopulateActiveAndModifiedWithSystemUser(referral);

      return referral;
    }
  }
}