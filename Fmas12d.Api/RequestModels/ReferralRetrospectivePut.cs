using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class ReferralRetrospectivePut : ReferralPut
  {
    internal override void MapToBusinessModel(Business.Models.ReferralUpdate model)
    {
      base.MapToBusinessModel(model);
      model.CreatedAt = CreatedAt.Value;      
    }

    [Required]
    public DateTimeOffset? CreatedAt { get; set; }
  }
}