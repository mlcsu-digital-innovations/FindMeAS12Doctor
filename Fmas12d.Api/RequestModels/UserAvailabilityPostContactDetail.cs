using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class UserAvailabilityPostContactDetail : UserAvailability
  {
    [Required]
    public int? ContactDetailId { get; set; }

    internal override Business.Models.UserAvailability MapToBusinessModel(int userId)
    {
      base.MapToBusinessModel(userId);
      _model.Location.ContactDetailId = ContactDetailId.Value;
      return _model;
    }    
  }
}