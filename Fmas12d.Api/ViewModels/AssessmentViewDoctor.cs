namespace Fmas12d.Api.ViewModels
{
  public class ExaminationViewDoctor
  {
    public ExaminationViewDoctor(Business.Models.User model)
    {
      DisplayName = model.DisplayName;
      GmcNumber = model.GmcNumber;
      Id = model.Id;      
    }

    public string DisplayName { get; set; }
    public int? GmcNumber { get; set; }
    public int Id { get; set; }
  }
}