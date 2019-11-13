using System;
using System.ComponentModel.DataAnnotations;

namespace Fmas12d.Api.RequestModels
{
  public class UserAvailability
  {
    protected Business.Models.UserAvailability _model;

    [Required]
    public DateTimeOffset? End { get; set; }
    [Required]
    public DateTimeOffset? Start { get; set; }

    internal virtual Business.Models.UserAvailability MapToBusinessModel(int userId)
    {
      _model = new Business.Models.UserAvailability();
      _model.End = End.Value;
      _model.UserId = userId;
      _model.Start = Start.Value;
      return _model;
    }
  }
}