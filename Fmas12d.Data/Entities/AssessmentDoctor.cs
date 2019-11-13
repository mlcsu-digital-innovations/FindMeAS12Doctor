﻿namespace Fmas12d.Data.Entities
{
  public partial class AssessmentDoctor : BaseEntity, IAssessmentDoctor
  {
    public int? AttendanceConfirmedByUserId { get; set; }
    public virtual User AttendanceConfirmedByUser { get; set; }
    public virtual User DoctorUser { get; set; }
    public int DoctorUserId { get; set; }    
    public virtual Assessment Assessment { get; set; }
    public int AssessmentId { get; set; }
    public virtual AssessmentDoctorStatus Status { get; set; }
    public int StatusId { get; set; }
  }
}