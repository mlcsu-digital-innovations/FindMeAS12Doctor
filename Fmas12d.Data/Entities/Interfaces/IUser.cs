using System;

namespace Fmas12d.Data.Entities
{
  public interface IUser
  {
    string DisplayName { get; set; }
    int? GenderTypeId { get; set; }
    int? GmcNumber { get; set; }
    bool HasReadTermsAndConditions { get; set; }
    string IdentityServerIdentifier { get; set; }
    int OrganisationId { get; set; }
    int ProfileTypeId { get; set; }
    int? Section12ApprovalStatusId { get; set; }
    DateTimeOffset? Section12ExpiryDate { get; set; }
  }
}