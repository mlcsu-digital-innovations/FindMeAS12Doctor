using System;

namespace Fmas12d.Api.ViewModels
{
  public class ReferralEdit
  {
    public ReferralEdit() {}
    public ReferralEdit(Business.Models.Referral model)
    {
      if (model == null) return ;

      CreatedAt = model.CreatedAt;
      Id = model.Id;
      LeadAmhpUserDisplayName = model.LeadAmhpUser?.DisplayName;
      LeadAmhpUserId = model.LeadAmhpUserId;
      PatientAlternativeIdentifier = model.Patient?.AlternativeIdentifier;
      PatientCcgId = model.Patient?.CcgId;
      PatientCcgName = model.Patient?.Ccg?.Name;
      PatientGpPracticeNameAndPostcode = model.PatientGpPracticeNameAndPostcode;
      PatientGpPracticeId = model.Patient?.GpPracticeId;
      PatientId = model.Patient?.Id;
      PatientNhsNumber = model.Patient?.NhsNumber;
      PatientResidentialPostcode = model.Patient?.ResidentialPostcode;
      StatusName = model.StatusName;
    }

    public DateTimeOffset CreatedAt { get; set; }
    public int Id { get; set; }
    public string LeadAmhpUserDisplayName {get; set;}
    public int LeadAmhpUserId { get; set; }    
    public string PatientAlternativeIdentifier { get; set; }
    public int? PatientCcgId { get; set; }
    public string PatientCcgName {get; set;}
    public string PatientGpPracticeNameAndPostcode {get; set;}
    public int? PatientGpPracticeId { get; set; }
    public int? PatientId { get; set; }
    public long? PatientNhsNumber { get; set; }
    public string PatientResidentialPostcode { get; set; }
    public string StatusName { get; set; }
  }
}