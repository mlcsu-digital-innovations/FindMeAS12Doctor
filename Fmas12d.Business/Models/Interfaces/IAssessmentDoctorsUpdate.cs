using System;
using System.Collections.Generic;

namespace Fmas12d.Business.Models
{
  public interface IAssessmentDoctorsUpdate
  {
    int Id { get; set; }
    IEnumerable<int> UserIds { get; set; }    
  }
}