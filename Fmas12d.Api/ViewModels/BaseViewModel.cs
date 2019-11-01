using System;

namespace Mep.Api.ViewModels
{
  public abstract class BaseViewModel
  {
    protected BaseViewModel() {}
    protected BaseViewModel(Business.Models.BaseModel model)
    {
      if (model == null) return;

      Id = model.Id;
      ModifiedAt = model.ModifiedAt;
      // TODO ModifiedByUser
      ModifiedByUserId = model.ModifiedByUserId;
      IsActive = model.IsActive;
    }

    public int Id { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    public virtual User ModifiedByUser { get; set; }
    public int ModifiedByUserId { get; set; }
    public bool IsActive { get; set; }
  }
}