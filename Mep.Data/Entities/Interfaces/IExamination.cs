using System;
using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface IExamination
  {
    string Address1 { get; set; }
    string Address2 { get; set; }
    string Address3 { get; set; }
    string Address4 { get; set; }
    ICcg Ccg { get; set; }
    int CcgId { get; set; }
    IUser CompletedByUser { get; set; }
    int? CompletedByUserId { get; set; }
    DateTimeOffset? CompletedTime { get; set; }
    IUser CompletionConfirmationByUser { get; set; }
    int? CompletionConfirmationByUserId { get; set; }
    IUser CreatedByUser { get; set; }
    int CreatedByUserId { get; set; }
    bool? IsSuccessful { get; set; }
    string MeetingArrangementComment { get; set; }
    DateTimeOffset MustBeCompletedBy { get; set; }
    INonPaymentLocation NonPaymentLocation { get; set; }
    int NonPaymentLocationId { get; set; }
    string Postcode { get; set; }
    IReferral Referral { get; set; }
    int ReferralId { get; set; }
    DateTimeOffset ScheduledTime { get; set; }
    ISpeciality Speciality { get; set; }
    int SpecialityTypeId { get; set; }
    int? UnsuccesfulExaminationTypeId { get; set; }
    IUnsuccessfulExaminationType UnsuccessfulExaminationType { get; set; }
    IList<IUserExaminationClaim> UserExaminationClaims { get; set; }
    IList<IUserExaminationNotification> UserExaminationNotifications { get; set; }
  }
}