namespace Fmas12d.Business.Migrations.Seeds.SpineServiceModels
{
  public class SpineServiceRelationship
  {
    public SpineServiceDateType[] Date { get; set; }
    public string Status { get; set; }
    public SpineServiceTarget Target { get; set; }
    public string Id { get; set; }
    public string UniqueRelId { get; set; }
  }
}