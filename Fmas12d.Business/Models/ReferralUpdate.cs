using System;

namespace Fmas12d.Business.Models
{
  public class ReferralUpdate
  {    
    public DateTimeOffset? CreatedAt { get; set; }
    public int Id { get; set; }
    public int LeadAmhpUserId { get; set; }
  }
}