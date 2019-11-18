using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentDoctorsPost
  {
    public AssessmentDoctorsPost() { }

    public void MapToBusinessModel(Business.Models.IAssessmentDoctorsUpdate model)
    {
      model.UserIds = UserIds;      
    }

    [Required]
    [MinLength(1)]
    public IEnumerable<int> UserIds { get; set; }
  }
}