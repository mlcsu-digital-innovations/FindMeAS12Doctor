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

    internal override Business.Models.UserAvailability MapToBusinessModel(int userId)
    {
      base.MapToBusinessModel(userId);
      _model.Location.Latitude = Latitude.Value;
      _model.Location.Longitude = Longitude.Value;
      return _model;
    }
  }
}