using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  public partial class User : BaseEntity, IUser
  {
    public virtual IList<BankDetail> BankDetails { get; set; }
    [InverseProperty("CompletedByUser")]
    public virtual IList<Examination> CompletedExaminations { get; set; }
    [InverseProperty("CompletionConfirmationByUser")]
    public virtual IList<Examination> CompletionConfirmationExaminations { get; set; }
    public virtual IList<ContactDetail> ContactDetails { get; set; }
    public virtual IList<DoctorStatus> DoctorStatuses { get; set; }
    public int? GmcNumber { get; set; }
    public bool HasReadTermsAndConditions { get; set; }
    [MaxLength(50)]
    [Required]
    public string IdentityServerIdentifier { get; set; }
    public virtual IList<OnCallUser> OnCallUsers { get; set; }
    public virtual Organisation Organisation { get; set; }
    public int OrganisationId { get; set; }
    public virtual IList<PaymentMethod> PaymentMethods { get; set; }
    public virtual ProfileType ProfileType { get; set; }
    public int ProfileTypeId { get; set; }
    public virtual IList<Referral> Referrals { get; set; }
    public virtual Section12ApprovalStatus Section12ApprovalStatus { get; set; }
    public int? Section12ApprovalStatusId { get; set; }
    public DateTimeOffset? Section12ExpiryDate { get; set; }
    public virtual IList<UserSpeciality> UserSpecialities { get; set; }
    public virtual IList<UserExaminationClaim> UserExaminationClaims { get; set; }
    public virtual IList<UserExaminationNotification> UserExaminationNotifications { get; set; }
  }
}
