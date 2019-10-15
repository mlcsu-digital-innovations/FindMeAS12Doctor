using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Mep.Api.ViewModels
{
  public class Examination : BaseViewModel
  {
    [Required]
    [MaxLength(200)]
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }
    public virtual Ccg Ccg { get; set; }
    public int CcgId { get; set; }
    public int? CompletedByUserId { get; set; }
    public virtual User CompletedByUser { get; set; }
    public DateTimeOffset? CompletedTime { get; set; }
    public int? CompletionConfirmationByUserId { get; set; }
    public virtual User CompletionConfirmationByUser { get; set; }
    public virtual User CreatedByUser { get; set; }
    public int CreatedByUserId { get; set; }
    public virtual IList<ExaminationDetailType> DetailTypes { get; set; }
    public bool? IsSuccessful { get; set; }
    [MaxLength(2000)]
    public string MeetingArrangementComment { get; set; }
    public DateTimeOffset MustBeCompletedBy { get; set; }
    public int? NonPaymentLocationId { get; set; }
    public virtual NonPaymentLocation NonPaymentLocation { get; set; }
    [Required]
    [MaxLength(10)]
    public string Postcode { get; set; }
    public int ReferralId { get; set; }
    public virtual Referral Referral { get; set; }
    public DateTimeOffset ScheduledTime { get; set; }
    public int SpecialityId { get; set; }
    public virtual Speciality Speciality { get; set; }
    public int? UnsuccessfulExaminationTypeId { get; set; }
    public UnsuccessfulExaminationType UnsuccessfulExaminationType { get; set; }
    public virtual IList<UserExaminationClaim> UserExaminationClaims { get; set; }
    public virtual IList<UserExaminationNotification> UserExaminationNotifications { get; set; }
    public virtual GenderType PreferredDoctorGenderType { get; set; }
    public int? PreferredDoctorGenderTypeId { get; set; }
  }
}