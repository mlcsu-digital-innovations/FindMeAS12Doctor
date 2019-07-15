using System;
using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public interface IUser
  {
    IList<IBankDetail> BankDetails { get; set; }
    IList<IExamination> CompletedExaminations { get; set; }
    IList<IExamination> CompletionConfirmationExaminations { get; set; }
    IList<IContactDetail> ContactDetails { get; set; }
    IList<IDoctorStatus> DoctorStatuses { get; set; }
    int? GmcNumber { get; set; }
    bool HasReadTermsAndConditions { get; set; }
    string IdentityServerIdentifier { get; set; }
    IList<IOnCallUser> OnCallUsers { get; set; }
    IOrganisation Organisation { get; set; }
    int OrganisationId { get; set; }
    IList<IPaymentMethod> PaymentMethods { get; set; }
    IProfileType ProfileType { get; set; }
    int ProfileTypeId { get; set; }
    IList<IReferral> Referrals { get; set; }
    ISection12ApprovalStatus Section12ApprovalStatus { get; set; }
    int? Section12ApprovalStatusId { get; set; }
    DateTimeOffset? Section12ExpiryDate { get; set; }
    IList<IUserExaminationClaim> UserExaminationClaims { get; set; }
    IList<IUserExaminationNotification> UserExaminationNotifications { get; set; }
    IList<IUserSpeciality> UserSpecialities { get; set; }
  }
}