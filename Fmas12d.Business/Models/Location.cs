using Fmas12d.Business.Models.SearchModels;

namespace Fmas12d.Business.Models
{
  public class Location
  {
    public Location() { }
    public Location(PostcodeIoResult model)
    {
      if (model == null) return;

      Latitude = model.Result.Latitude;
      Longitude = model.Result.Longitude;
      Postcode = model.Result.Postcode;
    }

    public Location(Data.Entities.UserAvailability entity)
    {
      if (entity == null) return;

      ContactDetailId = entity.ContactDetailId;
      ContactDetail = new ContactDetail(entity.ContactDetail, false);
      Latitude = entity.Latitude;
      Longitude = entity.Longitude;
      Postcode = entity.Postcode;
    }

    public int? ContactDetailId { get; set; }
    public ContactDetail ContactDetail { get; set; }
    public bool HasContactDetailId { get { return ContactDetailId != null; } }
    public bool HasPostcode { get { return !string.IsNullOrWhiteSpace(Postcode); } }
    public string Postcode { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }    
  }
}