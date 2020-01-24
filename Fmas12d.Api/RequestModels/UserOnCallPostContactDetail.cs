using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class UserOnCallPostContactDetail : UserOnCall
  {
    [Required]
    public int? ContactDetailId { get; set; }

    internal override void MapToBusinessModel(Business.Models.IUserOnCall model)
    {
      base.MapToBusinessModel(model);
      if (model != null && model.Location != null)
      {
        model.Location.ContactDetailId = ContactDetailId.Value;
      }
    }
  }
}