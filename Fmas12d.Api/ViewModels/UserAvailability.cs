using System;
using Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels
{
  public class UserAvailability
  {
    public UserAvailability() { }

    public UserAvailability(IUserAvailability model)
    {
      MapFromBusinessModel(model);
    }

    public DateTimeOffset End { get; set; }
    public Location Location { get; set; }
    public int Id { get; set; }
    public bool IsOnCall { get; set; }
    public DateTimeOffset Start { get; set; }

    public int StatusId { get; set; }
    public string StatusName { get; set; }

    internal virtual void MapFromBusinessModel(IUserAvailability model)
    {
      if (model == null) return;

      End = model.End;
      Location = new Location(model.Location);
      Id = model.Id;
      IsOnCall = model.StatusId == Business.Models.UserAvailabilityStatus.ON_CALL;
      Start = model.Start;
      StatusId = model.StatusId;
      StatusName = model.Status.Name;
    }

    public static Func<IUserAvailability, UserAvailability> ProjectFromModel
    {
      get
      {
        return model => new UserAvailability(model);
      }
    }
  }
}