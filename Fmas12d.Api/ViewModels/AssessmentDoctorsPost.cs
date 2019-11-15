using System.Collections.Generic;
using Fmas12d.Business.Models;

namespace Fmas12d.Api.ViewModels
{
  public class AssessmentDoctorsPost
  {
    public AssessmentDoctorsPost() { }

    public AssessmentDoctorsPost(IAssessmentDoctorsUpdate model)
    {
      if (model == null) return;

      Id = model.Id;
      UserIds = model.UserIds;
    }

    public int Id  { get; set; }
    public IEnumerable<int> UserIds { get; set; }
  }
}