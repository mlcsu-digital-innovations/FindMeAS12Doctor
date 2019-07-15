using System.Collections.Generic;

namespace Mep.Data.Entities.Audit
{
  public partial class UnsuccessfulExaminationTypeAudit : NameDescription, IUnsuccessfulExaminationType
  {
    public virtual IList<IExamination> Examinations { get; set; }
  }
}
