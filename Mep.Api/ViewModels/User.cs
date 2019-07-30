using System;

namespace Mep.Api.ViewModels
{
  public class User : BaseViewModel
  {           
    public int? GmcNumber { get; set; }
    public bool HasReadTermsAndConditions { get; set; }
    public string IdentityServerIdentifier { get; set; }
    public int? OrganisationId { get; set; }
    public int? ProfileTypeId { get; set; }
    public int? Section12ApprovalStatusId { get; set; }
    public DateTimeOffset? Section12ExpiryDate { get; set; }
    public string DisplayName {get; set;}
  }
}