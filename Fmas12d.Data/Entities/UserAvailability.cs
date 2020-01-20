using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Fmas12d.Data.Entities
{
  [Table("UserAvailabilities")]
  public partial class UserAvailability : BaseEntity, IUserAvailability
  {
    public int? ContactDetailId { get; set; }
    public virtual ContactDetail ContactDetail { get; set; }
    public DateTimeOffset End { get; set; }    
    [Column(TypeName = "decimal(8,6)")]
    public decimal Latitude { get; set; }
    [Column(TypeName = "decimal(9,6)")]
    public decimal Longitude { get; set; }
    public DateTimeOffset? OnCallConfirmationSentAt { get; set; }
    public DateTimeOffset? OnCallConfirmationReceivedAt { get; set; }
    public string OnCallRejectedReason { get; set; }
    public bool? OnCallIsConfirmed { get; set; }
    public string Postcode { get; set; }
    public DateTimeOffset Start { get; set; }
    public int UserAvailabilityStatusId { get; set; }
    public UserAvailabilityStatus UserAvailabilityStatus { get; set; }
    public virtual User User { get; set; }
    public int UserId { get; set; }        
  }
}
