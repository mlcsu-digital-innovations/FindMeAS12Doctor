using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class UserAvailabilityPostLocation : UserAvailability
  {
    [Required]
    [Range(-90, 90)]
    public decimal? Latitude { get; set; }
    [Required]
    [Range(-180, 180)]
    public decimal? Longitude { get; set; }

    internal override void MapToBusinessModel(Business.Models.IUserAvailability model)
    {
      base.MapToBusinessModel(model);
      if (model != null && model.Location != null)
      {
        model.Location.Latitude = Latitude.Value;
        model.Location.Longitude = Longitude.Value;
      }
    }
  }
}