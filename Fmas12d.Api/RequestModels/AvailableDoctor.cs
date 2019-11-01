using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Mep.Api.RequestModels
{
  public class AvailableDoctor
  {
    public DateTimeOffset AvailabilityStart { get; set; }
    public DateTimeOffset AvailabilityEnd { get; set; }

    [Column(TypeName = "decimal(8,6)")]
    public decimal Latitude { get; set; }
    [Column(TypeName = "decimal(9,6)")]
    public decimal Longitude { get; set; }
    [Column(TypeName = "decimal(8,6)")]

    public double DistanceInMiles { get; set; }

    [Required]
    public int ModifiedByUserId { get; set; }
    public string DoctorName { get; set; }
  }
}