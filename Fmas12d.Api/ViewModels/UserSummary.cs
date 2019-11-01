using System;

namespace Mep.Api.ViewModels
{
  public class UserSummary
  {
    public UserSummary() { }

    public UserSummary(string displayName, int id)
    {
      DisplayName = displayName;
      Id = id;
    }

    public string DisplayName { get; set; }
    public int Id { get; set; }
  }
}