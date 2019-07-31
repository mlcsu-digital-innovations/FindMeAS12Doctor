namespace Mep.Business.Migrations.Seeds.SpineServiceModels
{
  public class SpineServiceRole
  {
    public string Id { get; set; }
    public string UniqueRoleId { get; set; }
    public bool PrimaryRole { get; set; }

    public SpineServiceDateType[] Date { get; set; }
    public string Status { get; set; }
  }
}