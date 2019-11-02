using System;
using System.Collections.Generic;

namespace Fmas12d.Api.ViewModels
{
  public class Referral : BaseViewModel
  {
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset DefaultToBeCompletedBy { get; set; }
    public bool IsPlannedAssessment { get; set; }
    public int CreatedByUserId { get; set; }
    public int LeadAmhpUserId { get; set; }
    public int PatientId { get; set; }
    public int ReferralStatusId { get; set; }
    public virtual IList<Assessment> Assessments { get; set; }
    public virtual Patient Patient { get; set; }
    public virtual ReferralStatus ReferralStatus { get; set; }
    public virtual User CreatedByUser { get; set; }
    public virtual User LeadAmhpUser { get; set; }
  }
}