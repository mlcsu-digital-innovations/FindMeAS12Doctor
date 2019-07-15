using System;
using System.Collections.Generic;

namespace Mep.Data.Entities
{
  public partial class ReferralAudit : BaseAudit, IReferral
  {
    public DateTimeOffset CreatedAt { get; set; }
    // public virtual User CreatedByUser { get; set; }
    public int CreatedByUserId { get; set; }
    // public virtual IList<ExaminationAudit> Examinations { get; set; }
    // public virtual PatientAudit Patient { get; set; }
    public int PatientId { get; set; }
    // public virtual ReferralStatusAudit ReferralStatus { get; set; }
    public int ReferralStatusId { get; set; }
  }
}
