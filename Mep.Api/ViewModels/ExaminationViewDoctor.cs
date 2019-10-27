namespace Mep.Api.ViewModels
{
  public class ExaminationViewDoctor
  {
    public ExaminationViewDoctor(Business.Models.User user)
    {
      DisplayName = user.DisplayName;
      Id = user.Id;
    }

    public string DisplayName { get; set; }
    public int Id { get; set; }
  }
}