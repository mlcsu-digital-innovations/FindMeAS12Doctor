using System;

namespace Fmas12d.Data.Entities
{
   public interface IAssessmentDoctor
  {    
     int AssessmentId { get; set; }    
     int? AttendanceConfirmedByUserId { get; set; }
     int? ContactDetailId { get; set; }
     decimal? Distance { get; set; }
     int DoctorUserId { get; set; }
     bool? HasAccepted { get; set; }
     decimal Latitude { get; set; }
     decimal Longitude { get; set; }
     string Postcode { get; set; }    
     DateTimeOffset? RespondedAt { get; set; }
     int StatusId { get; set; }
  }
}