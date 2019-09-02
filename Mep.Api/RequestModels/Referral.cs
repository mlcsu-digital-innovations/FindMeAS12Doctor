using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mep.Api.RequestModels
{
  public abstract class Referral
  {
    // nullable ints [Required] for foreign keys - enable model validation ?
    public DateTimeOffset CreatedAt { get; set; }
    public virtual UserPut CreatedByUser { get; set; }
    [Required]
    public int? CreatedByUserId { get; set; }
    public virtual IList<Examination> Examinations { get; set; }
    public virtual Patient Patient { get; set; }
    [Required]
    public int? PatientId { get; set; }
    public virtual ReferralStatus ReferralStatus { get; set; }
    [Required]
    public int? ReferralStatusId { get; set; }
    public virtual User LeadAmhpUser { get; set; }
    public int LeadAmhpUserId { get; set; }
    public bool IsPlannedExamination { get; set; }
  }
}