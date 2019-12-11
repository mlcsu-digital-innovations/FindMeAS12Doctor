namespace Fmas12d.Api.ViewModels
{
  public class AssessmentViewDoctor
  {
    public AssessmentViewDoctor(Business.Models.AssessmentDoctor model)
    {
      DisplayName = model.DoctorUser.DisplayName;
      Distance = model.Distance;
      DoctorId = model.DoctorUserId;
      GmcNumber = model.DoctorUser.GmcNumber;
      HasAccepted = model.HasAccepted ?? false;
      HasResponded = model.RespondedAt == null ? false : true;
      Id = model.Id;      
      IsRegistered = model.IsRegistered;
      IsS12 = model.IsS12;
      KnownLocation = new Location(model.KnownLocation);
    }

    public string DisplayName { get; set; }
    public decimal? Distance { get; set; }
    public int DoctorId { get; set; }
    public int? GmcNumber { get; set; }
    public bool HasAccepted { get; set; }
    public bool HasResponded { get; set; }
    public bool? IsRegistered { get; set; }
    public bool? IsS12 { get; set; }
    public int Id { get; set; }
    public Location KnownLocation { get; set; }
  }
}