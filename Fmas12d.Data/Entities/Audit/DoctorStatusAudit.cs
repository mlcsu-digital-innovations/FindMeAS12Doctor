using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Data.Entities
{
  [Table("DoctorStatusesAudit")]
  public partial class DoctorStatusAudit : BaseAudit, IDoctorStatus
  {
    public DateTimeOffset AvailabilityEnd { get; set; }
    public DateTimeOffset AvailabilityStart { get; set; }    
    public DateTimeOffset? ExtendedAvailabilityEnd1 { get; set; }
    public DateTimeOffset? ExtendedAvailabilityEnd2 { get; set; }
    public DateTimeOffset? ExtendedAvailabilityEnd3 { get; set; }
    [Column(TypeName = "decimal(8,6)")]
    public decimal? ExtendedAvailabilityLatitude1 { get; set; }
    [Column(TypeName = "decimal(8,6)")]
    public decimal? ExtendedAvailabilityLatitude2 { get; set; }
    [Column(TypeName = "decimal(8,6)")]
    public decimal? ExtendedAvailabilityLatitude3 { get; set; }
    [Column(TypeName = "decimal(9,6)")]
    public decimal? ExtendedAvailabilityLongitude1 { get; set; }
    [Column(TypeName = "decimal(9,6)")]
    public decimal? ExtendedAvailabilityLongitude2 { get; set; }
    [Column(TypeName = "decimal(9,6)")]
    public decimal? ExtendedAvailabilityLongitude3 { get; set; }    
    public DateTimeOffset? ExtendedAvailabilityStart1 { get; set; }    
    public DateTimeOffset? ExtendedAvailabilityStart2 { get; set; }    
    public DateTimeOffset? ExtendedAvailabilityStart3 { get; set; }
    [Column(TypeName = "decimal(8,6)")]
    public decimal Latitude { get; set; }
    [Column(TypeName = "decimal(9,6)")]
    public decimal Longitude { get; set; }
    public int UserId { get; set; }
  }
}
