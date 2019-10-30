using System.ComponentModel.DataAnnotations;
using Mep.Business.Models;

namespace Mep.Api.ViewModels
{
  public class PatientSearch
  {
    public PatientSearch() {}
    public PatientSearch(Business.Models.Patient model)
    {
      AlternativeIdentifier = model.AlternativeIdentifier;
      CcgId = model.CcgId;
      CcgName = model.Ccg?.Name;
      CurrentReferralId = model.GetCurrentReferralId();
      GpPracticeId = model.GpPracticeId;
      GpPracticeNameAndPostcode = model.GpPracticeNameAndPostcode;
      NhsNumber = model.NhsNumber;
      PatientId = model.Id;
      ResidentialPostcode = model.ResidentialPostcode;      
    }

    public string AlternativeIdentifier { get; set; }
    public int? CcgId { get; set; }
    public string CcgName{ get; set; }
    public int? CurrentReferralId { get; set; }
    public int? GpPracticeId { get; set; }
    public string GpPracticeNameAndPostcode { get; set; }
    public long? NhsNumber { get; set; }
    public int? PatientId { get; set; }
    public string ResidentialPostcode { get; set; }    
  }
}