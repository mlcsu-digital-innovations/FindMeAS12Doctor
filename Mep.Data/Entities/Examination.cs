using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  public partial class Examination : BaseEntity, IExamination
  {
    [Required]
    [MaxLength(200)]
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Address4 { get; set; }
    public virtual ICcg Ccg { get; set; }
    public int CcgId { get; set; }
    public int? CompletedByUserId { get; set; }
    [ForeignKey("CompletedByUserId")]
    public virtual IUser CompletedByUser { get; set; }
    public DateTimeOffset? CompletedTime { get; set; }
    public int? CompletionConfirmationByUserId { get; set; }
    [ForeignKey("CompletionConfirmationByUserId")]
    public virtual IUser CompletionConfirmationByUser { get; set; }
    public int CreatedByUserId { get; set; }
    public virtual IUser CreatedByUser { get; set; }
    public bool? IsSuccessful { get; set; }
    [MaxLength(2000)]
    public string MeetingArrangementComment { get; set; }
    public DateTimeOffset MustBeCompletedBy { get; set; }
    public int NonPaymentLocationId { get; set; }
    public virtual INonPaymentLocation NonPaymentLocation { get; set; }
    [Required]
    [MaxLength(10)]
    public string Postcode { get; set; }
    public int ReferralId { get; set; }
    public virtual IReferral Referral { get; set; }
    public DateTimeOffset ScheduledTime { get; set; }
    public int SpecialityTypeId { get; set; }
    public ISpeciality Speciality { get; set; }
    public int? UnsuccesfulExaminationTypeId { get; set; }
    public IUnsuccessfulExaminationType UnsuccessfulExaminationType { get; set; }
    public virtual IList<IUserExaminationClaim> UserExaminationClaims { get; set; }
    public virtual IList<IUserExaminationNotification> UserExaminationNotifications { get; set; }
  }
}
