using Mep.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mep.Business.Migrations.Seeds
{
  internal class ExaminationSeeder : SeederBase<Examination>
  {
    internal Examination Create(
      string address1,
      string ccgName,
      string createdByUserName,
      string postcode,      
      string address2 = null,
      string address3 = null,
      string address4 = null,
      string completedByUserName = null,
      string completionConfirmationByUserName = null,
      DateTimeOffset? completedTime = null,
      List<ExaminationDetail> details = null,
      bool? isSuccessful = null,
      string meetingArrangementComment = null,
      DateTimeOffset? mustBeCompletedBy = null,
      int? nonPaymentLocationId = null,
      int? preferredDoctorGenderTypeId = null,
      DateTimeOffset? scheduledTime = null,
      int? specialityId = null,
      int? unsuccessfulExaminationTypeId = null,
      List<UserExaminationNotification> userExaminationNotifications = null
    )
    {
      Examination examination = new Examination
      {
        Address1 = address1,
        Address2 = address2,
        Address3 = address3,
        Address4 = address4,
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
        UnsuccessfulExaminationTypeId =
          unsuccessfulExaminationTypeId == null ? null : unsuccessfulExaminationTypeId,
        UserExaminationNotifications = userExaminationNotifications
      };

      PopulateActiveAndModifiedWithSystemUser(examination);

      return examination;
    }

    /// <summary>
    /// Also deletes all ExaminationDetails
    /// </summary>
    internal override void DeleteSeeds()
    {
      Context.ExaminationDetails.RemoveRange(
        Context.ExaminationDetails.ToList()
      );      
      base.DeleteSeeds();
    }
  }
}