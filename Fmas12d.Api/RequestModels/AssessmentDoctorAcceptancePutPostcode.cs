using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentDoctorAcceptancePutPostcode : AssessmentDoctorAcceptance
  {
    [Required]
    public string Postcode { get; set; }

    internal override Business.Models.AssessmentDoctor MapToBusinessModel()
    {
      base.MapToBusinessModel();
      _model.Postcode = Postcode;
      return _model;
    }
  }
}