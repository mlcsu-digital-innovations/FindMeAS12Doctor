namespace Mep.Business.Models
{
  public interface INameDescription : IBaseModel
  {
    string Description { get; set; }
    string Name { get; set; }
  }
}