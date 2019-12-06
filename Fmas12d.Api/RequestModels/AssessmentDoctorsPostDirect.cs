using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentDoctorsPostDirect
  {
    public AssessmentDoctorsPostDirect() { }

    [Required]
    public int? UserId { get; set; }
  }
}