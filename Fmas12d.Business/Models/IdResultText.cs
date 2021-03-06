namespace Fmas12d.Business.Models
{
  public class IdResultText
  {
    public IdResultText() { }

    public IdResultText(Data.Entities.Ccg entity)
    {
      Id = entity.Id;
      ResultText = entity.Name;
    }
        
    public IdResultText(Data.Entities.GpPractice entity)
    {
      Id = entity.Id;
      ResultText = $"{entity.Name}, {entity.Postcode}";
    }

    public int Id { get; set; }
    public string ResultText { get; set; }
  }
}