﻿using System;

namespace Mep.Data.Entities
{
  public interface IUserExaminationNotification
  {
    int ExaminationId { get; set; }
    bool? HasAccepted { get; set; }
    int NotificationTextId { get; set; }
    DateTimeOffset? RespondedAt { get; set; }
    int UserId { get; set; }
  }
}