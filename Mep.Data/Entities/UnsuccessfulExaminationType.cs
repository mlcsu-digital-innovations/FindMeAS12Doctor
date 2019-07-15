using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public partial class UnsuccessfulExaminationType : NameDescription, IUnsuccessfulExaminationType
    {
    public virtual IList<IExamination> Examinations { get; set; }
  }
}
