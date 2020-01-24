using System;
using System.Collections.Generic;

namespace Fmas12d.Business.Models
{
  public interface IUser
  {
    IList<Referral> AmhpReferrals { get; set; }
    IList<BankDetail> BankDetails { get; set; }
    IList<Assessment> CompletedAssessments { get; set; }
    IList<Assessment> CompletionConfirmationAssessments { get; set; }
    IList<ContactDetail> ContactDetails { get; set; }
    IList<Assessment> CreatedAssessments { get; set; }
    string DisplayName { get; set; }
    IList<UserAvailability> UserAvailabilities { get; set; }
    GenderType GenderType { get; set; }
    int? GenderTypeId { get; set; }
    int? GmcNumber { get; set; }
    bool HasReadTermsAndConditions { get; set; }
    string IdentityServerIdentifier { get; set; }
    Organisation Organisation { get; set; }
    int OrganisationId { get; set; }
    IList<PaymentMethod> PaymentMethods { get; set; }
    ProfileType ProfileType { get; set; }
    int ProfileTypeId { get; set; }
    IList<Referral> Referrals { get; set; }
    Section12ApprovalStatus Section12ApprovalStatus { get; set; }
    int? Section12ApprovalStatusId { get; set; }
    DateTimeOffset? Section12ExpiryDate { get; set; }
    IList<UserSpeciality> UserSpecialities { get; set; }
    IList<UserAssessmentClaim> UserAssessmentClaims { get; set; }
    IList<UserAssessmentClaim> UserAssessmentClaimSelections { get; set; }
    IList<UserAssessmentNotification> UserAssessmentNotifications { get; set; }
    string GenderName { get; }
    bool IsAmhp { get; }
    bool IsDoctor { get; }
  }
}
