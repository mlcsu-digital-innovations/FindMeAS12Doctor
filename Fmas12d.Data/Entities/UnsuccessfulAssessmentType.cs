using System.Collections.Generic;

namespace Fmas12d.Data.Entities
{
  public partial class UnsuccessfulAssessmentType : 
    NameDescription, IUnsuccessfulAssessmentType
    {
    public virtual IList<Assessment> Assessments { get; set; }
  }
}
