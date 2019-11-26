using System;

namespace Fmas12d.Data.Entities
{
  public interface IAssessmentDoctor
  {    
    int AssessmentId { get; set; }
    int? AttendanceConfirmedByUserId { get; set; }
    int DoctorUserId { get; set; }    
    bool? HasAccepted { get; set; }        
    DateTimeOffset? RespondedAt { get; set; }
    int StatusId { get; set; }
  }
}