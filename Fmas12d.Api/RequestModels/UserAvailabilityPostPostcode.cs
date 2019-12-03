using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class UserAvailabilityPostPostcode : UserAvailability
  {
    [Required]
    public string Postcode { get; set; }

    internal override Business.Models.UserAvailability MapToBusinessModel(int userId)
    {
      base.MapToBusinessModel(userId);
      _model.Location.Postcode = Postcode;
      return _model;
    }
  }
}