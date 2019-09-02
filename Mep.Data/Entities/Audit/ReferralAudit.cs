﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("ReferralsAudit")]
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

    //public virtual User LeadAmhpUser {get; set;}
    public int LeadAmhpUserId {get; set;}
    public bool IsPlannedExamination {get; set;}
  }
}
