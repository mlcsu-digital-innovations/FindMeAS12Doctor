namespace Fmas12d.Api.RequestModels
{
  public class PatientPost : Patient
  {
    public PatientPost() {}

    public PatientPost(Business.Models.Patient model) : base(model)
    {
    }
  }
}