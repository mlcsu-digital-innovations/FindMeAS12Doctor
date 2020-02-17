namespace Fmas12d.Api.ViewModels
{
    public class UnsuccessfulAssessmentType
    {
        public UnsuccessfulAssessmentType(Business.Models.UnsuccessfulAssessmentType model) {

          if (model == null) {
            return;
          }
          Name = model.Name;

        }

      public string Name { get; set; }

    }
}