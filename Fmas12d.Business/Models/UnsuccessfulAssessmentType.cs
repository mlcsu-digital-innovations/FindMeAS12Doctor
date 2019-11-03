using System.Collections.Generic;
namespace Fmas12d.Business.Models
{
  public class UnsuccessfulAssessmentType : NameDescription
  {
    public const int REFUSED_ENTRY = 1;
    public const int PATIENT_UNAVAILABLE = 2;
    public virtual IList<Assessment> Assessments { get; set; }
  }
}