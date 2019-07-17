using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class UnsuccessfulExaminationType : NameDescription
  {
    public virtual IList<Examination> Examinations { get; set; }
  }
}