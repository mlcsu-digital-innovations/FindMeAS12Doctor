using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class UserAvailabilityPostPostcode : UserAvailability
  {
    [Required]
    public string Postcode { get; set; }

    internal override void MapToBusinessModel(Business.Models.IUserAvailability model)
    {
      base.MapToBusinessModel(model);
      if (model != null && model.Location != null)
      {
        model.Location.Postcode = Postcode;
      }
    }
  }
}