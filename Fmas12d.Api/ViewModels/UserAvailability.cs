using System;
using Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels
{
  public class UserAvailability
  {
    public UserAvailability() { }

    public UserAvailability(IUserAvailability model)
    {
      MapFromBuinessModel(model);
    }

    public DateTimeOffset End { get; set; }
    public Location Location { get; set; }
    public int Id { get; set; }
    public DateTimeOffset Start { get; set; }

    internal virtual void MapFromBuinessModel(IUserAvailability model)
    {
      if (model == null) return;

      End = model.End;
      Location = new Location(model.Location);
      Id = model.Id;
      Start = model.Start;
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