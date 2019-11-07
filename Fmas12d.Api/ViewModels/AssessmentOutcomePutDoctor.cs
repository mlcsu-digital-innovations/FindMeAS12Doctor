namespace Fmas12d.Api.ViewModels
{
  public class AssessmentOutcomePutDoctor
  {
    public AssessmentOutcomePutDoctor() { }

    public AssessmentOutcomePutDoctor(Business.Models.AssessmentOutcomeDoctor model)
    {
      if (model == null) return;
      
      Attended = model.Attended;
      Id = model.Id;
    }

    public int Id { get; set; }
    public bool? Attended { get; set; }
  }
}