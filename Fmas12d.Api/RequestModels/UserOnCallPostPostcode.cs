using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class UserOnCallPostPostcode : UserOnCall
  {
    [Required]
    public string Postcode { get; set; }

    internal override void MapToBusinessModel(Business.Models.IUserOnCall model)
    {
      base.MapToBusinessModel(model);
      if (model != null && model.Location != null)
      {
        model.Location.Postcode = Postcode;
      }
    }
  }
}