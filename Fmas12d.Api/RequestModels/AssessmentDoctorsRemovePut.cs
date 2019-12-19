using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentDoctorsRemovePut
  {
    public AssessmentDoctorsRemovePut() { }

    public void MapToBusinessModel(Business.Models.IAssessmentDoctorsRemove model)
    {
      model.UserIds = UserIds;      
    }

    [Required]
    [MinLength(1)]
    public IEnumerable<int> UserIds { get; set; }
  }
}