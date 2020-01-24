using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class UserOnCall : UserAvailability
  {

    [Required]
    [Range(1, int.MaxValue)]
    public int? UserId { get; set; }

    internal virtual void MapToBusinessModel(Business.Models.IUserOnCall model)
    {
      if (model == null) return;

      base.MapToBusinessModel(model);      
      model.UserId = UserId.Value;
    }
  }
}