using Fmas12d.Business.Models.SearchModels;

namespace Fmas12d.Business.Models
{
  public class Location
  {
    public Location() {}
    public Location(PostcodeIoResult postcodeIoResult)
    {
      Postcode = postcodeIoResult.Result.Postcode;
      Latitude = postcodeIoResult.Result.Latitude;
      Longitude = postcodeIoResult.Result.Longitude;
    }

    public int? ContactDetailId { get; set; }
    public virtual ContactDetail ContactDetail { get; set; }
    public string Postcode { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
  }
}