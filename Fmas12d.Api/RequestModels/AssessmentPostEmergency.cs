using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class ExaminationPostEmergency : Examination
  {
    public ExaminationPostEmergency() {}
    public ExaminationPostEmergency(Business.Models.ExaminationCreate model) : base(model)
    {
      MustBeCompletedBy = model.MustBeCompletedBy;
    }

    internal override Business.Models.ExaminationCreate MapToBusinessModel()
    {
      Business.Models.ExaminationCreate model = base.MapToBusinessModel();
      model.MustBeCompletedBy = MustBeCompletedBy;
      return model;
    }

    [Required]
    public DateTimeOffset? MustBeCompletedBy { get; set; }
  }
}