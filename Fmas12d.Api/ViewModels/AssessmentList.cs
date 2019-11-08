using System;

namespace Fmas12d.Api.ViewModels
{
  public class AssessmentList
  {
    public AssessmentList() {}
    public AssessmentList(Business.Models.Assessment model)
    {
      this.DateTime = model.DateTime;
      this.Id = model.Id;
      this.Postcode = model.Postcode;

    }

    public DateTimeOffset DateTime { get; set; }
    public int Id { get; set; }
    public string Postcode { get; set; }

    public static Func<Business.Models.Assessment, AssessmentList> ProjectFromModel
    {
      get
      {
        return model => new AssessmentList(model);
      }
    }
  }


}