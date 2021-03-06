﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("ReferralsAudit")]
  public partial class ReferralAudit : BaseAudit, IReferral
  {
    public DateTimeOffset CreatedAt { get; set; }
    public int CreatedByUserId { get; set; }
    public bool IsPlannedAssessment {get; set;}
    public int PatientId { get; set; }
    public int ReferralStatusId { get; set; }
    public int LeadAmhpUserId {get; set;}    
  }
}
