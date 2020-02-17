using System;
using Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels
{
  public class UserOnCall : UserAvailability
  {
    public UserOnCall()
     : base()
    { }

    public UserOnCall(IUserOnCall model)
      : base(model)
    {
      MapFromBusinessModel(model);
    }

    public int GmcNumber { get; set; }
    public DateTimeOffset? OnCallConfirmationReceivedAt { get; set; }
    public DateTimeOffset? OnCallConfirmationSentAt { get; set; }
    public bool? OnCallIsConfirmed { get; set; }
    public string OnCallRejectedReason { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }


    internal virtual void MapFromBusinessModel(IUserOnCall model)
    {
      if (model == null) return;

      GmcNumber = model.User.GmcNumber.HasValue ? model.User.GmcNumber.Value : 0;
      OnCallConfirmationReceivedAt = model.OnCallConfirmationReceivedAt;
      OnCallConfirmationSentAt = model.OnCallConfirmationSentAt;
      OnCallIsConfirmed = model.OnCallIsConfirmed;
      OnCallRejectedReason = model.OnCallRejectedReason;
      UserId = model.UserId;
      UserName = model.User.DisplayName;
    }

    public new static Func<IUserOnCall, UserOnCall> ProjectFromModel
    {
      get
      {
        return model => new UserOnCall(model);
      }
    }
  }
}