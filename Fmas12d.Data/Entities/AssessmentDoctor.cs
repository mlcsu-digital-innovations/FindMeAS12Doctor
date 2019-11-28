using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fmas12d.Data.Entities
{
  public partial class AssessmentDoctor : BaseEntity, IAssessmentDoctor
  {    
    public virtual Assessment Assessment { get; set; }
    public int AssessmentId { get; set; }    
    public int? AttendanceConfirmedByUserId { get; set; }
    public virtual User AttendanceConfirmedByUser { get; set; }
    public int? ContactDetailId { get; set; }
    public virtual ContactDetail ContactDetail { get; set; }
    [Column(TypeName = "decimal(9,6)")]
    public decimal? Distance { get; set; }
    public virtual User DoctorUser { get; set; }
    public int DoctorUserId { get; set; }
    public bool? HasAccepted { get; set; }
    [Column(TypeName = "decimal(8,6)")]
    public decimal Latitude { get; set; }
    [Column(TypeName = "decimal(9,6)")]
    public decimal Longitude { get; set; }
    public string Postcode { get; set; }    
    public DateTimeOffset? RespondedAt { get; set; }
    public virtual AssessmentDoctorStatus Status { get; set; }
    public int StatusId { get; set; }
  }
}
