namespace Fmas12d.Api.RequestModels
{
  public class AssessmentDoctorAcceptance
  {
    protected Business.Models.AssessmentDoctor _model;

    public int AssessmentId { get; set; }
    public bool HasAccepted { get; set; }
    public int UserId { get; set; }    

    internal virtual Business.Models.AssessmentDoctor MapToBusinessModel()
    {
      _model = new Business.Models.AssessmentDoctor
      {
        AssessmentId = AssessmentId,        
        DoctorUserId = UserId,
        HasAccepted = HasAccepted
      };
      return _model;
    }
  }
}