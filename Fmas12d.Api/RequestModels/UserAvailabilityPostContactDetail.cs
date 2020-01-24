using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class UserAvailabilityPostContactDetail : UserAvailability
  {
    [Required]
    public int? ContactDetailId { get; set; }

    internal override void MapToBusinessModel(Business.Models.IUserAvailability model)
    {
      base.MapToBusinessModel(model);
      if (model != null && model.Location != null)
      {
        model.Location.ContactDetailId = ContactDetailId.Value;
      }
    }
  }
}