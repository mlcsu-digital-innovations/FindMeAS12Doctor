using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class ReferralRetrospectivePost : ReferralPost
  {
    internal override void MapToBusinessModel(Business.Models.ReferralCreate model)
    {
      base.MapToBusinessModel(model);
      model.CreatedAt = CreatedAt.Value;      
    }

    [Required]
    public DateTimeOffset? CreatedAt { get; set; }
  }
}