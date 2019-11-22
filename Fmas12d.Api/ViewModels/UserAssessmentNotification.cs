using System;

namespace Fmas12d.Api.ViewModels
{
  public class UserAssessmentNotification
  {
    public UserAssessmentNotification(Business.Models.UserAssessmentNotification model)
    {
      if (model == null) return;

      AssessmentId = model.AssessmentId;
      HasAccepted = model.HasAccepted;
      NotificationTextId = model.NotificationTextId;
      RespondedAt = model.RespondedAt;
      UserId = model.UserId;
    }

    public int AssessmentId { get; set; }
    public bool? HasAccepted { get; set; }
    public int NotificationTextId { get; set; }
    public DateTimeOffset? RespondedAt { get; set; }
    public int UserId { get; set; }

    public static Func<Business.Models.UserAssessmentNotification, UserAssessmentNotification> ProjectFromModel
    {
      get
      {
        return model => new UserAssessmentNotification(model);
      }
    }     
  }
}