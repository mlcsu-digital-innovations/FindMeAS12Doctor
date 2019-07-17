using System;
using System.Collections.Generic;
namespace Mep.Business.Models
{
  public class Referral : BaseModel
  {
    public DateTimeOffset CreatedAt { get; set; }
    // public virtual User CreatedByUser { get; set; }
    public int CreatedByUserId { get; set; }
    // public virtual IList<Examination> Examinations { get; set; }
    // public virtual Patient Patient { get; set; }
    public int PatientId { get; set; }
    // public virtual ReferralStatus ReferralStatus { get; set; }
    public int ReferralStatusId { get; set; }
  }
}