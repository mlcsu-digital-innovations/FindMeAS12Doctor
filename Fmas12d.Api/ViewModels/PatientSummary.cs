namespace Fmas12d.Api.ViewModels
{
  public class PatientSummary
  {
    public PatientSummary() { }
    public PatientSummary(Business.Models.Referral model)
    {
      PatientIdentifier = model.PatientIdentifier;
    }

    public string PatientIdentifier { get; set; }
  }
}