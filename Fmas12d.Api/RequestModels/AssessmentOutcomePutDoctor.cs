using BusinessModels = Fmas12d.Business.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class AssessmentOutcomePutDoctor
  {
    [Range(1, int.MaxValue)]
    public int Id { get; set; }
    [Required]
    public bool? Attended { get; set; }

    internal BusinessModels.AssessmentOutcomeDoctor MapToBusinessModel()
    {
      BusinessModels.AssessmentOutcomeDoctor model = new BusinessModels.AssessmentOutcomeDoctor()
      {
        Attended = Attended.Value,
        Id = Id
      };

      return model;
    }
  }
}