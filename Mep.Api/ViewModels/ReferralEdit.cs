using System;

namespace Mep.Api.ViewModels
{
  public class ReferralEdit
  {
    public DateTimeOffset CreatedAt { get; set; }
    public int Id { get; set; }
    public string LeadAmhpUserDisplayName {get; set;}
    public int LeadAmhpUserId { get; set; }    
    public string PatientAlternativeIdentifier { get; set; }
    public int? PatientCcgId { get; set; }
    public string PatientCcgName {get; set;}
    public string PatientGpPracticeNameAndPostcode {get; set;}
    public int? PatientGpPracticeId { get; set; }
    public long? PatientNhsNumber { get; set; }
    public string PatientResidentialPostcode { get; set; }
    public string Status { get; set; }
  }
}