namespace Fmas12d.Api.ViewModels
{
  public abstract class NameDescription : BaseViewModel
  {
    protected NameDescription() {}
    protected NameDescription(Business.Models.NameDescription model) : base(model)
    {
      if (model == null) return;
      
      Description = model.Description;
      Name = model.Name;
    }
    public string Description { get; set; }
    public string Name { get; set; }    
  }
}