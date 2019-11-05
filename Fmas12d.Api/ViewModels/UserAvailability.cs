using System;

namespace Fmas12d.Api.ViewModels
{
  public class UserAvailability
  {
    public UserAvailability() {}

    public UserAvailability(Business.Models.IUserAvailability model)
    {
      MapFromBuinessModel(model);
    }

    public int? ContactDetailId { get; set; }
    public DateTimeOffset End { get; set; }
    public int Id { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }    
    public string Postcode { get; set; }    
    public DateTimeOffset Start { get; set; }    

    internal virtual void MapFromBuinessModel(Business.Models.IUserAvailability model)
    {
      if (model == null) return;
      
      ContactDetailId = model.ContactDetailId;
      End = model.End;
      Id = model.Id;
      Longitude = model.Longitude;
      Latitude = model.Latitude;
      Postcode = model.Postcode;
      Start = model.Start;      
    }
  }
}