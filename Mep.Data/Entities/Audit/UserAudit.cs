using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("UsersAudit")]
  public partial class UserAudit : BaseAudit, IUser
  {
    // public virtual IList<BankDetailAudit> BankDetails { get; set; }
    // [InverseProperty("CompletedByUser")]
    // public virtual IList<ExaminationAudit> CompletedExaminations { get; set; }
    // [InverseProperty("CompletionConfirmationByUser")]
    // public virtual IList<ExaminationAudit> CompletionConfirmationExaminations { get; set; }
    // public virtual IList<ContactDetailAudit> ContactDetails { get; set; }
    // public virtual IList<DoctorStatusAudit> DoctorStatuses { get; set; }
    public int? GmcNumber { get; set; }
    public bool HasReadTermsAndConditions { get; set; }
    [MaxLength(50)]
    [Required]
    public string IdentityServerIdentifier { get; set; }
    // public virtual IList<OnCallUserAudit> OnCallUsers { get; set; }
    // public virtual OrganisationAudit Organisation { get; set; }
    public int OrganisationId { get; set; }
    // public virtual IList<PaymentMethodAudit> PaymentMethods { get; set; }
    // public virtual ProfileTypeAudit ProfileType { get; set; }
    public int ProfileTypeId { get; set; }
    // public virtual IList<ReferralAudit> Referrals { get; set; }
    // public virtual Section12ApprovalStatusAudit Section12ApprovalStatus { get; set; }
    public int? Section12ApprovalStatusId { get; set; }
    public DateTimeOffset? Section12ExpiryDate { get; set; }
    // public virtual IList<UserSpecialityAudit> UserSpecialities { get; set; }
    // public virtual IList<UserExaminationClaimAudit> UserExaminationClaims { get; set; }
    // public virtual IList<UserExaminationNotificationAudit> UserExaminationNotifications { get; set; }

    // public virtual GenderType GenderType { get; set; }
    public int? GenderTypeId { get; set; }
  }
}
