using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Business.Models
{
  public class AssessmentDoctorsRemove : IAssessmentDoctorsRemove
  {
    public AssessmentDoctorsRemove() { }

    [Required]
    public int Id { get; set; }

    [Required]
    [MinLength(1)]
    public IEnumerable<int> UserIds { get; set; }
  }
}