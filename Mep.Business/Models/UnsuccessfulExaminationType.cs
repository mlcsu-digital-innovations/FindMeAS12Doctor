using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class UnsuccessfulExaminationType : NameDescription
  {
    public const int REFUSED_ENTRY = 1;
    public virtual IList<Examination> Examinations { get; set; }
  }
}