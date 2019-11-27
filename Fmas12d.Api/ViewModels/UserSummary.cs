using System;

namespace Fmas12d.Api.ViewModels
{
  public class UserSummary
  {
    public UserSummary() { }

    public UserSummary(Business.Models.User model) {
      DisplayName = model.DisplayName;
      Id = model.Id;
    }

    public string DisplayName { get; set; }
    public int Id { get; set; }
  }
}