using System;
using System.Collections.Generic;

namespace Fmas12d.Data.Entities
{
  public partial class Referral : BaseEntity, IReferral
  {
    public DateTimeOffset CreatedAt { get; set; }
    public virtual User CreatedByUser { get; set; }
    public int CreatedByUserId { get; set; }    
    public virtual IList<Examination> Examinations { get; set; }
    public bool IsPlannedExamination {get; set;}
    public virtual Patient Patient { get; set; }
    public int PatientId { get; set; }
    public virtual ReferralStatus ReferralStatus { get; set; }
    public int ReferralStatusId { get; set; }
    public virtual User LeadAmhpUser {get; set;}
    public int LeadAmhpUserId {get; set;}    
  }
}
