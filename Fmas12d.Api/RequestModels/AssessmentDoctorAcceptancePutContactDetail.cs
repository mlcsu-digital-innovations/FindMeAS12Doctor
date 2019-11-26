using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentDoctorAcceptancePutContactDetail : AssessmentDoctorAcceptance
  {
    [Required]
    public int? ContactDetailId { get; set; }

    internal override Business.Models.AssessmentDoctor MapToBusinessModel()
    {
      base.MapToBusinessModel();
      _model.ContactDetailId = ContactDetailId.Value;
      return _model;
    }  
  }
}