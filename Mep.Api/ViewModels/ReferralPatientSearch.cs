using System.ComponentModel.DataAnnotations;
namespace Mep.Api.ViewModels
{
  public class ReferralPatientSearch
  {
    [MaxLength(200)]
    public string AlternativeIdentifier { get; set; }
    public int? CcgId { get; set; }
    public int? GpPracticeId { get; set; }
    public long? NhsNumber { get; set; }
    [MaxLength(10)]
    public string ResidentialPostcode { get; set; }
    public int? CurrentReferralId { get; set; }
    public int? PatientId { get; set; }
    public string GpPracticeDetails { get; set; }
    public string CcgDetails { get; set; }
  }
}