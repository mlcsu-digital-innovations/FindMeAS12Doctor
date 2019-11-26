namespace Fmas12d.Api.ViewModels
{
  public class AssessmentViewDoctor
  {
    public AssessmentViewDoctor(Business.Models.AssessmentDoctor model)
    {
      DisplayName = model.DoctorUser.DisplayName;
      Distance = model.Distance;
      GmcNumber = model.DoctorUser.GmcNumber;
      Id = model.Id;      
      KnownLocation = new Location(model.KnownLocation);
    }

    public string DisplayName { get; set; }
    public decimal? Distance { get; set; }
    public int? GmcNumber { get; set; }
    public int Id { get; set; }
    public Location KnownLocation { get; set; }
  }
}