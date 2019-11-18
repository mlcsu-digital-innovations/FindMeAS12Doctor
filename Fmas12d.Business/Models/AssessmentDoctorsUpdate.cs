using System.Collections.Generic;

namespace Fmas12d.Business.Models
{
  public class AssessmentDoctorsUpdate : IAssessmentDoctorsUpdate
  {
    public AssessmentDoctorsUpdate() { }

    public int Id { get; set; }
    public IEnumerable<int> UserIds { get; set; }
  }
}