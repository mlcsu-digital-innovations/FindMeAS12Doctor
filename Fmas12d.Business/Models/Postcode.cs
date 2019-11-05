using Fmas12d.Business.Models.SearchModels;

namespace Fmas12d.Business.Models
{
  public class Postcode
  {
    public Postcode() {}
    public Postcode(PostcodeIoResult postcodeIoResult)
    {
      Code = postcodeIoResult.Result.Postcode;
      Latitude = postcodeIoResult.Result.Latitude;
      Longitude = postcodeIoResult.Result.Longitude;
    }

    public string Code { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
  }
}