using BusinessModels = Fmas12d.Business.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class ExaminationOutcomePutDoctor
  {
    [Range(1, int.MaxValue)]
    public int Id { get; set; }
    [Required]
    public bool? Attended { get; set; }

    internal BusinessModels.ExaminationOutcomeDoctor MapToBusinessModel()
    {
      BusinessModels.ExaminationOutcomeDoctor model = new BusinessModels.ExaminationOutcomeDoctor()
      {
        Attended = Attended.Value,
        Id = Id
      };

      return model;
    }
  }
}