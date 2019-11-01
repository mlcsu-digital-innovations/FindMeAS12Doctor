namespace Mep.Api.ViewModels
{
  public abstract class NameDescription : BaseViewModel
  {
    protected NameDescription() {}
    protected NameDescription(Business.Models.ExaminationDetailType model) : base(model)
    {
      if (model == null) return;
      
      Description = model.Description;
      Name = model.Description;
    }
    public string Description { get; set; }
    public string Name { get; set; }    
  }
}