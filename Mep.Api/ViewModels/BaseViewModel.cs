using System;

namespace Mep.Api.ViewModels
{
  public abstract class BaseViewModel
  {
    public int Id { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    public int ModifiedByUserId { get; set; }
    public bool IsActive { get; set; }
  }
}