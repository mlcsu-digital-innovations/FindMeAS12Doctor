using Fmas12d.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fmas12d.Business.Migrations.Seeds
{
  internal class AssessmentSeeder : SeederBase<Assessment>
  {
    internal Assessment Create(
      string address1,
      string amhpUserName,
      string ccgName,
      string createdByUserName,
      string postcode,      
      string address2 = null,
      string address3 = null,
      string address4 = null,
      string completedByUserName = null,
      string completionConfirmationByUserName = null,
      DateTimeOffset? completedTime = null,
      List<AssessmentDetail> details = null,
      List<AssessmentDoctor> doctors = null,
      bool? isSuccessful = null,
      string meetingArrangementComment = null,
      DateTimeOffset? mustBeCompletedBy = null,
      int? nonPaymentLocationId = null,
      int? preferredDoctorGenderTypeId = null,
      DateTimeOffset? scheduledTime = null,
      int? specialityId = null,
      int? unsuccessfulAssessmentTypeId = null,
      List<UserAssessmentNotification> userAssessmentNotifications = null
    )
    {
      Assessment assessment = new Assessment
      {
        Address1 = address1,
        Address2 = address2,
        Address3 = address3,
        Address4 = address4,
        AmhpUserId = GetUserByDisplayName(amhpUserName).Id,
        CcgId = GetCcgByName(ccgName).Id,
        CompletedByUserId =
          completedByUserName == null ? (int?)null : GetUserByDisplayName(completedByUserName).Id,
        CompletedTime = completedTime == null ? null : completedTime,
        CompletionConfirmationByUserId =
          completionConfirmationByUserName == null
            ? (int?)null
            : GetUserByDisplayName(completionConfirmationByUserName).Id,
        CreatedByUserId = GetUserByDisplayName(createdByUserName).Id,
        Details = details,
        Doctors = doctors,
        IsSuccessful = isSuccessful == null ? null : isSuccessful,
        MeetingArrangementComment = meetingArrangementComment,
        MustBeCompletedBy = mustBeCompletedBy == null ? null : mustBeCompletedBy,
        NonPaymentLocationId =
          nonPaymentLocationId == null ? null : nonPaymentLocationId,
        Postcode = postcode,
        PreferredDoctorGenderTypeId =
          preferredDoctorGenderTypeId == null ? null : preferredDoctorGenderTypeId,
        ScheduledTime = scheduledTime == null ? null : scheduledTime,
        SpecialityId = specialityId == null ? (int?)null : specialityId,
        UnsuccessfulAssessmentTypeId =
          unsuccessfulAssessmentTypeId == null ? null : unsuccessfulAssessmentTypeId,
        UserAssessmentNotifications = userAssessmentNotifications
      };

      PopulateActiveAndModifiedWithSystemUser(assessment);

      return assessment;
    }

    /// <summary>
    /// Also deletes all AssessmentDetails, AssessmentDoctors
    /// </summary>
    internal override void DeleteSeeds()
    {
      Context.AssessmentDetails.RemoveRange(
        Context.AssessmentDetails.ToList()
      );      
      Context.AssessmentDoctors.RemoveRange(
        Context.AssessmentDoctors.ToList()
      );      
      base.DeleteSeeds();
    }
  }
}