using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities.Audit
{
  public partial class UserAudit : BaseAudit, IUser
  {
    public virtual IList<IBankDetail> BankDetails { get; set; }
    [InverseProperty("CompletedByUser")]
    public virtual IList<IExamination> CompletedExaminations { get; set; }
    [InverseProperty("CompletionConfirmationByUser")]
    public virtual IList<IExamination> CompletionConfirmationExaminations { get; set; }
    public virtual IList<IContactDetail> ContactDetails { get; set; }
    public virtual IList<IDoctorStatus> DoctorStatuses { get; set; }
    public int? GmcNumber { get; set; }
    public bool HasReadTermsAndConditions { get; set; }
    [MaxLength(50)]
    [Required]
    public string IdentityServerIdentifier { get; set; }
    public virtual IList<IOnCallUser> OnCallUsers { get; set; }
    public virtual IOrganisation Organisation { get; set; }
    public int OrganisationId { get; set; }
    public virtual IList<IPaymentMethod> PaymentMethods { get; set; }
    public virtual IProfileType ProfileType { get; set; }
    public int ProfileTypeId { get; set; }
    public virtual IList<IReferral> Referrals { get; set; }
    public virtual ISection12ApprovalStatus Section12ApprovalStatus { get; set; }
    public int? Section12ApprovalStatusId { get; set; }
    public DateTimeOffset? Section12ExpiryDate { get; set; }
    public virtual IList<IUserSpeciality> UserSpecialities { get; set; }
    public virtual IList<IUserExaminationClaim> UserExaminationClaims { get; set; }
    public virtual IList<IUserExaminationNotification> UserExaminationNotifications { get; set; }
  }
}
