namespace Mep.Api.ViewModels
{
  public class PatientPost : Patient
  {
    public PatientPost() {}

    public PatientPost(Business.Models.Patient model) : base(model)
    {
    }
  }
}