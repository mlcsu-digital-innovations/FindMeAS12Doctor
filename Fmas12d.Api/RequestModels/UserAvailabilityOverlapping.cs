using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class UserAvailabilityOverlapping : UserAvailability
  {
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }

    internal override void MapToBusinessModel(Business.Models.IUserAvailability model)
    {
      base.MapToBusinessModel(model);

      if (model != null) {
        Id = model.Id;
        End = model.End;
        Start = model.Start;
      }     
    }
  }
}