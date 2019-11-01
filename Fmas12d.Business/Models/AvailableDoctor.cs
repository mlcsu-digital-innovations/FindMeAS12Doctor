using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mep.Business.Models
{
  public class AvailableDoctor : BaseModel
  {
    public DateTimeOffset AvailabilityStart { get; set; }
    public DateTimeOffset AvailabilityEnd { get; set; }

    [Column(TypeName = "decimal(8,6)")]
    public decimal Latitude { get; set; }
    [Column(TypeName = "decimal(9,6)")]
    public decimal Longitude { get; set; }
    [Column(TypeName = "decimal(8,6)")]

    public double DistanceInMiles { get; set; }

    public string DoctorName {get; set;}
  }
}