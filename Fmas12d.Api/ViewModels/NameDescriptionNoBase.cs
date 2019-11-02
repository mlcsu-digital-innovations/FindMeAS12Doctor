namespace Fmas12d.Api.ViewModels
{
  public abstract class NameDescriptionNoBase
  {
    protected NameDescriptionNoBase() {}
    protected NameDescriptionNoBase(Business.Models.AssessmentDetailType model)
    {
      if (model == null) return;
      
      Description = model.Description;
      Id = model.Id;
      Name = model.Description;
    }
    public string Description { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }    
  }
}