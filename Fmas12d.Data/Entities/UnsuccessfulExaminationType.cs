using System.Collections.Generic;

namespace Fmas12d.Data.Entities
{
  public partial class UnsuccessfulExaminationType : 
    NameDescription, IUnsuccessfulExaminationType
    {
    public virtual IList<Examination> Examinations { get; set; }
  }
}
