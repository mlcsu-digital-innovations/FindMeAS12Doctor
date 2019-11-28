using System;

namespace Fmas12d.Api.ViewModels
{
  public class Location
  {
    public enum LocationType
    {
      LatLng = 1,
      Postcode = 2,
      ContactDetail = 3
    }

    public Location() { }
    public Location(Business.Models.Location model)
    {
      if (model == null) return;

      ContactDetailId = model.ContactDetailId;
      ContactDetailTypeName = model?.ContactDetail?.ContactDetailType?.Name;
      Latitude = model.Latitude;
      Longitude = model.Longitude;
      Postcode = model.Postcode;
    }

    public int? ContactDetailId { get; set; }
    public string ContactDetailTypeName { get; set; }
    public string Postcode { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string Type
    {
      get
      {
        if (ContactDetailId == null)
        {
          if (Postcode == null)
          {
            return Enum.GetName(typeof(LocationType), LocationType.LatLng);
          }
          return Enum.GetName(typeof(LocationType), LocationType.Postcode);
        }
        return Enum.GetName(typeof(LocationType), LocationType.ContactDetail);
      }
    }
  }
}