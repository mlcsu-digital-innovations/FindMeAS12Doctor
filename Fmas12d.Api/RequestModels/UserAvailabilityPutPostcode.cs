using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class UserAvailabilityPutPostcode : UserAvailabilityPostPostcode
  {
    [Required]
    public bool? IsActive { get; set; }

    internal override Business.Models.UserAvailability MapToBusinessModel(int userId)
    {
      base.MapToBusinessModel(userId);
      _model.IsActive = IsActive.Value;
      return _model;
    }  
  }
}