using System;
using System.Collections.Generic;

namespace Mep.Data.Entities.Audit
{
  public partial class ReferralAudit : BaseAudit, IReferral
  {
    public DateTimeOffset CreatedAt { get; set; }
    public virtual IUser CreatedByUser { get; set; }
    public int CreatedByUserId { get; set; }
    public virtual IList<IExamination> Examinations { get; set; }
    public virtual IPatient Patient { get; set; }
    public int PatientId { get; set; }
    public virtual IReferralStatus ReferralStatus { get; set; }
    public int ReferralStatusId { get; set; }
  }
}
