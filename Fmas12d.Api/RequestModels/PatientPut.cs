namespace Fmas12d.Api.RequestModels
{
  public class PatientPut : Patient
  {
    public PatientPut() {}

    public PatientPut(Business.Models.Patient model) : base(model)
    {
    }    
  }
}