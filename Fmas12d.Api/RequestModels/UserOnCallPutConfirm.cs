using System;

namespace Fmas12d.Api.RequestModels
{
  public class UserOnCallPutConfirm
  {
    protected bool _isConfirmed;
    public UserOnCallPutConfirm()
    {
      _isConfirmed = true;
    }
    public bool IsConfirmed { get { return _isConfirmed; } }

    internal virtual void MapToBusinessModel(Business.Models.IUserOnCall model)
    {
      if (model == null) return;

      model.OnCallIsConfirmed = IsConfirmed;
      model.OnCallConfirmationReceivedAt = DateTimeOffset.Now;
    }    
  }
}