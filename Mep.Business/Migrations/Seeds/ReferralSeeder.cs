using Mep.Data.Entities;
using System;
using System.Collections.Generic;

namespace Mep.Business.Migrations.Seeds
{
  internal class ReferralSeeder : SeederBase<Referral>
  {
    #region Constants
    internal const string ALLOCATING_ADDRESS1 = "Baldwins Gate Filling Station";
    internal const string ALLOCATING_ADDRESS2 = "Newcastle Road";
    internal const string ALLOCATING_ADDRESS3 = "Baldwin's Gate";
    internal const string ALLOCATING_ADDRESS4 = "Newcastle";
    internal const string ALLOCATING_MEETING_ARRGANEMENT_COMMENT = "Allocating Meeting Arangement Comment";
    internal readonly DateTimeOffset ALLOCATING_MUST_BE_COMPLETED_BY =
      new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day,
                         15, 00, 00, 00, DateTimeOffset.Now.Offset);
    internal const string ALLOCATING_POSTCODE = "ST5 5DA";
    #endregion

    private readonly ExaminationSeeder _examinationSeeder = new ExaminationSeeder();
    private readonly UserExaminationNotificationSeeder _userExaminationNotificationSeeder = 
      new UserExaminationNotificationSeeder();

    /// <summary>
    /// Deletes all the following seeds because updating is too difficult:
    /// UserExaminationNotification
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

      AddAllocatingReferral();

    }

    private void AddAllocatingReferral()
    {
      Referral refAllocating = Add(
        alternativeIdentifier: PatientSeeder.ALTERNATIVE_IDENTIFIER_CCG_STOKE_ON_TRENT,
        createdAt: _now,
        leadAmhpName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
        referralStatusId: Models.ReferralStatus.ALLOCATING
      );
      Examination refAllocatingExam = _examinationSeeder.Create(
        address1: ALLOCATING_ADDRESS1,
        address2: ALLOCATING_ADDRESS2,
        address3: ALLOCATING_ADDRESS3,
        address4: ALLOCATING_ADDRESS4,
        ccgName: CcgSeeder.STOKE_ON_TRENT,
        createdByUserName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
        meetingArrangementComment: ALLOCATING_MEETING_ARRGANEMENT_COMMENT,
        mustBeCompletedBy: ALLOCATING_MUST_BE_COMPLETED_BY,
        postcode: ALLOCATING_POSTCODE
      );
      UserExaminationNotification refAllocatingExamUenAmhp = _userExaminationNotificationSeeder.Create(
          notificationTextId: Models.NotificationText.ASSIGNED_TO_EXAMINATION,
          userName: UserSeeder.DISPLAY_NAME_AMHP_MALE
      );
      refAllocatingExam.UserExaminationNotifications = 
        new List<UserExaminationNotification> {
          refAllocatingExamUenAmhp
      };
      refAllocating.Examinations = new List<Examination>
      {
        refAllocatingExam
      };
    }

    private void AddNewReferral()
    {
      Add(
        createdAt: _now,
        leadAmhpName: UserSeeder.DISPLAY_NAME_AMHP_MALE,
        nhsNumber: PatientSeeder.NHS_NUMBER_CCG_NORTH_STAFFORDSHIRE,
        referralStatusId: Models.ReferralStatus.NEW
      );
    }

    private Referral Add(
        DateTimeOffset createdAt,
        string leadAmhpName,
        int referralStatusId,
        bool isPlannedExamination = false,
        long? nhsNumber = null,
        string alternativeIdentifier = null)
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
      referral.IsPlannedExamination = isPlannedExamination;
      referral.LeadAmhpUserId = GetUserByDisplayName(leadAmhpName).Id;
      referral.ReferralStatusId = GetReferralStatus(referralStatusId).Id;
      PopulateActiveAndModifiedWithSystemUser(referral);

      return referral;
    }
  }
}