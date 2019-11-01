using System;

namespace Mep.Api.ViewModels
{
  public class IdNameDescription
  {
    public IdNameDescription() { }
    public IdNameDescription(Business.Models.INameDescription model)
    {
      Description = model.Description;
      Id = model.Id;
      Name = model.Name;
    }

    public string Description { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }

    public static Func<Business.Models.INameDescription, IdNameDescription> ProjectFromModel
    { get { return model => new IdNameDescription(model); } }
  }
}