using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("ExaminationsAudit")]
  public partial class ExaminationAudit : BaseAudit, IExamination
  {
    [Required]
    [MaxLength(200)]
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }
    // public virtual CcgAudit Ccg { get; set; }
    public int CcgId { get; set; }
    public int? CompletedByUserId { get; set; }
    // [ForeignKey("CompletedByUserId")]
    // public virtual UserAudit CompletedByUser { get; set; }
    public DateTimeOffset? CompletedTime { get; set; }
    public int? CompletionConfirmationByUserId { get; set; }
    // [ForeignKey("CompletionConfirmationByUserId")]
    // public virtual UserAudit CompletionConfirmationByUser { get; set; }
    public int CreatedByUserId { get; set; }
    // public virtual UserAudit CreatedByUser { get; set; }
    public bool? IsSuccessful { get; set; }
    [MaxLength(2000)]
    public string MeetingArrangementComment { get; set; }
    public DateTimeOffset MustBeCompletedBy { get; set; }
    public int? NonPaymentLocationId { get; set; }
    // public virtual NonPaymentLocationAudit NonPaymentLocation { get; set; }
    [Required]
    [MaxLength(10)]
    public string Postcode { get; set; }
    public int ReferralId { get; set; }
    // public virtual ReferralAudit Referral { get; set; }
    public DateTimeOffset ScheduledTime { get; set; }
    public int SpecialityId { get; set; }
    // [ForeignKey("CompletedByUserId")]
    // public SpecialityAudit Speciality { get; set; }
    public int? UnsuccessfulExaminationTypeId { get; set; }
    // public UnsuccessfulExaminationTypeAudit UnsuccessfulExaminationType { get; set; }
    // public virtual IList<UserExaminationClaimAudit> UserExaminationClaims { get; set; }
    // public virtual IList<UserExaminationNotificationAudit> UserExaminationNotifications { get; set; }

    public int GenderTypeId { get; set; }
  }
}
