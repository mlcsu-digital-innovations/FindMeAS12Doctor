using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  public partial class User : BaseEntity, IUser
  {
    [InverseProperty("User")]
    public virtual IList<BankDetail> BankDetails { get; set; }
    [InverseProperty("CompletedByUser")]
    public virtual IList<Examination> CompletedExaminations { get; set; }
    [InverseProperty("CompletionConfirmationByUser")]
    public virtual IList<Examination> CompletionConfirmationExaminations { get; set; }
    [InverseProperty("User")]    
    public virtual IList<ContactDetail> ContactDetails { get; set; }
    [InverseProperty("CreatedByUser")]
    public virtual IList<Examination> CreatedExaminations { get; set; }
    public virtual IList<DoctorStatus> DoctorStatuses { get; set; }
    public int? GmcNumber { get; set; }
    public bool HasReadTermsAndConditions { get; set; }
    [MaxLength(50)]
    [Required]
    public string IdentityServerIdentifier { get; set; }
    [InverseProperty("User")]
    public virtual IList<OnCallUser> OnCallUsers { get; set; }
    public virtual Organisation Organisation { get; set; }
    public int OrganisationId { get; set; }
    [InverseProperty("User")]
    public virtual IList<PaymentMethod> PaymentMethods { get; set; }
    public virtual ProfileType ProfileType { get; set; }
    public int ProfileTypeId { get; set; }
    [InverseProperty("CreatedbyUser")]
    public virtual IList<Referral> Referrals { get; set; }    
    public virtual Section12ApprovalStatus Section12ApprovalStatus { get; set; }
    public int? Section12ApprovalStatusId { get; set; }
    public DateTimeOffset? Section12ExpiryDate { get; set; }
    [InverseProperty("User")]
    public virtual IList<UserSpeciality> UserSpecialities { get; set; }
    [InverseProperty("User")]
    public virtual IList<UserExaminationClaim> UserExaminationClaims { get; set; }
    [InverseProperty("SelectedByUser")]
    public virtual IList<UserExaminationClaim> UserExaminationClaimSelections { get; set; }    
    [InverseProperty("User")]
    public virtual IList<UserExaminationNotification> UserExaminationNotifications { get; set; }
  }
}
