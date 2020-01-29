using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  public partial class User : BaseEntity, IUser
  {
    [InverseProperty("AmhpUser")]
    public virtual IList<Assessment> AmhpAssessments { get; set; }        
    [InverseProperty("LeadAmhpUser")]
    public virtual IList<Referral> AmhpReferrals { get; set; } 
    [InverseProperty("User")]
    public virtual IList<BankDetail> BankDetails { get; set; }
    [InverseProperty("CompletedByUser")]
    public virtual IList<Assessment> CompletedAssessments { get; set; }
    [InverseProperty("CompletionConfirmationByUser")]
    public virtual IList<Assessment> CompletionConfirmationAssessments { get; set; }
    [InverseProperty("User")]
    public virtual IList<ContactDetail> ContactDetails { get; set; }
    [InverseProperty("User")]
    public virtual IList<ContactDetailCcg> ContactDetailCcgs { get; set; }    
    [InverseProperty("CreatedByUser")]
    public virtual IList<Assessment> CreatedAssessments { get; set; }
    [MaxLength(256)]
    public string DisplayName { get; set; }
    [InverseProperty("DoctorUser")]
    public virtual IList<AssessmentDoctor> DoctorAssessments{ get; set; }    
    [InverseProperty("User")]
    public virtual IList<UserAvailability> UserAvailabilities { get; set; }
    [InverseProperty("AttendanceConfirmedByUser")]
    public virtual IList<AssessmentDoctor> AssessmentAttendanceConfirmations { get; set; }       
    public virtual GenderType GenderType { get; set; }
    public int? GenderTypeId { get; set; }    
    public int? GmcNumber { get; set; }
    public bool HasReadTermsAndConditions { get; set; }
    [MaxLength(50)]
    [Required]
    public string IdentityServerIdentifier { get; set; }
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
    public virtual IList<UserAssessmentClaim> UserAssessmentClaims { get; set; }
    [InverseProperty("User")]
    public virtual IList<UserAssessmentNotification> UserAssessmentNotifications { get; set; }
  }
}
