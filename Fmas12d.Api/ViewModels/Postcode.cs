namespace Fmas12d.Api.ViewModels
{
  public class Postcode
  {

    public Postcode() { }
    public Postcode(Business.Models.Postcode model)
    {
      if (model == null) return;

      Code = model.Code;
      Latitude = model.Latitude;
      Longitude = model.Longitude;
    }

    public string Code { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
  }
}