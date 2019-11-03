using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fmas12d.Business.Models
{
  public class User : BaseModel
  {
    public User() { }
    public User(Data.Entities.User entity) : base(entity)
    {
      if (entity == null) return;
      // TODO AmhpReferrals
      // TODO BankDetails
      // TODO CompletedAssessments
      // TODO CompletionConfirmationAssessments
      // TODO ContactDetails
      // TODO CreatedAssessments
      DisplayName = entity.DisplayName;
      // TODO DoctorStatuses
      // TODO GenderType
      GenderTypeId = entity.GenderTypeId;
      GmcNumber = entity.GmcNumber;
      HasReadTermsAndConditions = entity.HasReadTermsAndConditions;
      IdentityServerIdentifier = entity.IdentityServerIdentifier;
      // TODO OnCallUsers
      // TODO Organisation
      OrganisationId = entity.OrganisationId;
      // TODO PaymentMethods
      ProfileType = new ProfileType(entity.ProfileType);
      ProfileTypeId = entity.ProfileTypeId;
      // TODO Referrals
      // TODO Section12ApprovalStatus
      Section12ApprovalStatusId = entity.Section12ApprovalStatusId;
      Section12ExpiryDate = entity.Section12ExpiryDate;
      // TODO UserSpecialities
      // TODO UserAssessmentClaims
      // TODO UserAssessmentClaimSelections
      // TODO UserAssessmentNotification
    }

    public virtual IList<Referral> AmhpReferrals { get; set; }
    public virtual IList<BankDetail> BankDetails { get; set; }
    public virtual IList<Assessment> CompletedAssessments { get; set; }
    public virtual IList<Assessment> CompletionConfirmationAssessments { get; set; }
    public virtual IList<ContactDetail> ContactDetails { get; set; }
    public virtual IList<Assessment> CreatedAssessments { get; set; }
    [MaxLength(256)]
    public string DisplayName { get; set; }
    public virtual IList<DoctorStatus> DoctorStatuses { get; set; }
    public virtual GenderType GenderType { get; set; }
    public int? GenderTypeId { get; set; }
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
    public virtual IList<UserAssessmentClaim> UserAssessmentClaims { get; set; }
    public virtual IList<UserAssessmentClaim> UserAssessmentClaimSelections { get; set; }
    public virtual IList<UserAssessmentNotification> UserAssessmentNotifications { get; set; }

    public string GenderName { get { return GenderType?.Name; } }

    public bool IsAmhp { get { return ProfileType?.IsAmhp ?? false; } }

    public bool IsDoctor { get { return ProfileType?.IsDoctor ?? false; } }

    // Need EF core 3.1 fix: https://github.com/aspnet/EntityFrameworkCore/issues/18127
    // for this to work with .ThenInclude()
    public static Expression<Func<Data.Entities.User, User>> ProjectFromEntity
    {
      get
      {
        return userEntity => new User(userEntity);
      }
    }
  }
}