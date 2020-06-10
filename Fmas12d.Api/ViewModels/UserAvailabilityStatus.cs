using System;
using Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels
{
  public class UserAvailabilityStatus
  {
    public UserAvailabilityStatus() { }

    public UserAvailabilityStatus(IUserAvailabilityStatus model)
    {
      MapFromBusinessModel(model);
    }
        
    public bool IsOnCall { get; private set; }
    public string Name { get; set; }

    internal virtual void MapFromBusinessModel(IUserAvailabilityStatus model)
    {
      if (model == null) return;
                
      IsOnCall = model.IsOnCall;
      Name = model.Name;
    }

    public static Func<IUserAvailabilityStatus, UserAvailabilityStatus> ProjectFromModel
    {
      get
      {
        return model => new UserAvailabilityStatus(model);
      }
    }
  }
}