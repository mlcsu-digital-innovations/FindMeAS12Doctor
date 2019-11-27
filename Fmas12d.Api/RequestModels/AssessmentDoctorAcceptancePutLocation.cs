using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentDoctorAcceptancePutLocation : AssessmentDoctorAcceptance
  {
    [Required]
    [Range(-90, 90)]
    public decimal? Latitude { get; set; }
    [Required]
    [Range(-180, 180)]
    public decimal? Longitude { get; set; }

    internal override Business.Models.AssessmentDoctor MapToBusinessModel()
    {
      base.MapToBusinessModel();
      _model.Latitude = Latitude.Value;
      _model.Longitude = Longitude.Value;
      return _model;
    }
  }
}