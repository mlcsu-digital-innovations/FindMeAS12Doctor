using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class UserOnCallPutReject : UserOnCallPutConfirm
  {
    public UserOnCallPutReject() : base()
    {
      _isConfirmed = false;
    }

    [Required]
    [MaxLength(4000)]
    public string Reason { get; set; }

    internal override void MapToBusinessModel(Business.Models.IUserOnCall model)
    {
      if (model == null) return;
      base.MapToBusinessModel(model);
      model.OnCallRejectedReason = Reason;
    }      
  }

}