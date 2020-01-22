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
      base.MapToBusinessModel(model);
      if (model != null)
      {
        model.UserId = UserId.Value;
      }
    }
  }
}