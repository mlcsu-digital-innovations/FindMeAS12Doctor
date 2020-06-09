namespace Fmas12d.Api.ViewModels
{
  public class UserAvailabilityOverlapping
  {
    public bool IsOverlapping { get; set; }
    public string Message { get; set; }

    public UserAvailabilityOverlapping() {}

    public UserAvailabilityOverlapping(Business.Models.UserAvailabilityOverlapping businessModel) 
    {
      IsOverlapping = businessModel.IsOverlapping;
      Message = businessModel.Message;
    }   
  }
}