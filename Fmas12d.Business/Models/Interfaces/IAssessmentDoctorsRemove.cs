using System.Collections.Generic;

namespace Fmas12d.Business.Models
{
  public interface IAssessmentDoctorsRemove
  {
    int Id { get; set; }
    IEnumerable<int> UserIds { get; set; }
  }
}