using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  [Table("UserAvailabilitiesAudit")]
  public partial class UserAvailabilityAudit : BaseAudit, IUserAvailability
  {
    public int? ContactDetailId { get; set; }
    public DateTimeOffset End { get; set; }
    public DateTimeOffset Start { get; set; }
    [Column(TypeName = "decimal(8,6)")]
    public decimal Latitude { get; set; }
    [Column(TypeName = "decimal(9,6)")]
    public decimal Longitude { get; set; }
    public int UserId { get; set; }
    public string Postcode { get; set; }
    public int UserAvailabilityStatusId { get; set; }
  }
}
