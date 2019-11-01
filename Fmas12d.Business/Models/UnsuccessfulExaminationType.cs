using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class UnsuccessfulExaminationType : NameDescription
  {
    public const int REFUSED_ENTRY = 1;
    public const int PATIENT_UNAVAILABLE = 2;
    public virtual IList<Examination> Examinations { get; set; }
  }
}