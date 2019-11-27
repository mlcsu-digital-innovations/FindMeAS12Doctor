using System;

namespace Fmas12d.Business.Models
{
  public class AssessmentDoctor : BaseModel
  {
    public AssessmentDoctor() { }
    public AssessmentDoctor(Data.Entities.AssessmentDoctor entity) : base(entity)
    {
      if (entity == null) return;
      
      // TODO Assessment =
      AssessmentId = entity.AssessmentId;      
      AttendanceConfirmedByUserId = entity.Id;
      // TODO AttendanceConfirmedByUser =
      Distance = null;
      DoctorUser = entity.DoctorUser == null ? null : new User(entity.DoctorUser);
      DoctorUserId = entity.DoctorUserId;
      HasAccepted = entity.HasAccepted;
      RespondedAt = entity.RespondedAt;
      // TODO Status =
      StatusId = entity.StatusId;
    }    
    public virtual Assessment Assessment { get; set; }
    public int AssessmentId { get; set; }
    public int? AttendanceConfirmedByUserId { get; set; }
    public virtual User AttendanceConfirmedByUser { get; set; }
    public int? ContactDetailId { get; set; }
    public decimal? Distance { get; set; }
    public virtual User DoctorUser { get; set; }
    public int DoctorUserId { get; set; }
    public bool IsAvailable { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }    
    public bool? HasAccepted { get; set; }
    public string Postcode { get; set; }
    public DateTimeOffset? RespondedAt { get; set; }
    public virtual AssessmentDoctorStatus Status { get; set; }
    public int StatusId { get; set; }

    public bool IsAllocated { get { return StatusId == AssessmentDoctorStatus.ALLOCATED; } }    
    public bool IsSelected { get { return StatusId == AssessmentDoctorStatus.SELECTED; } }    
  }
}