using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  public partial class Assessment : BaseEntity, IAssessment
  {
    [Required]
    [MaxLength(200)]
    public string Address1 { get; set; }
    [MaxLength(500)]
    public string Address2 { get; set; }
    [MaxLength(500)]
    public string Address3 { get; set; }
    [MaxLength(500)]
    public string Address4 { get; set; }
    public User AmhpUser { get; set; }
    public int AmhpUserId { get; set; }
    public virtual Ccg Ccg { get; set; }
    public int? CcgId { get; set; }
    public virtual User CompletedByUser { get; set; }
    public int? CompletedByUserId { get; set; }
    public DateTimeOffset? CompletedTime { get; set; }
    public virtual User CompletionConfirmationByUser { get; set; }
    public int? CompletionConfirmationByUserId { get; set; }
    public virtual User CreatedByUser { get; set; }
    public int CreatedByUserId { get; set; }
    public IList<AssessmentDoctor> Doctors { get; set; }
    public virtual IList<AssessmentDetail> Details { get; set; }
    public bool? IsSuccessful { get; set; }
    [MaxLength(2000)]
    public string MeetingArrangementComment { get; set; }
    public DateTimeOffset? MustBeCompletedBy { get; set; }
    public int? NonPaymentLocationId { get; set; }
    public virtual NonPaymentLocation NonPaymentLocation { get; set; }
    [Required]
    [MaxLength(10)]
    public string Postcode { get; set; }
    public virtual GenderType PreferredDoctorGenderType { get; set; }
    public int? PreferredDoctorGenderTypeId { get; set; }
    public virtual Referral Referral { get; set; }
    public int ReferralId { get; set; }
    public DateTimeOffset? ScheduledTime { get; set; }
    public Speciality Speciality { get; set; }
    public int? SpecialityId { get; set; }
    public int? UnsuccessfulAssessmentTypeId { get; set; }
    public UnsuccessfulAssessmentType UnsuccessfulAssessmentType { get; set; }
    public virtual IList<UserAssessmentClaim> UserAssessmentClaims { get; set; }
    public virtual IList<UserAssessmentNotification> UserAssessmentNotifications { get; set; }

    [NotMapped]
    public bool HasDetails { get { return Details != null && Details.Count > 0; } }

    [NotMapped]
    public bool HasUserAssessmentNotifications
    {
      get
      {
        return UserAssessmentNotifications != null &&
               UserAssessmentNotifications.Count > 0;
      }
    }
  }
}
