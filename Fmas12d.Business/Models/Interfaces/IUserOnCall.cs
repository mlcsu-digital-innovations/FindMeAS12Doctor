using System;

namespace Fmas12d.Business.Models
{
  public interface IUserOnCall : IUserAvailability
  {
    DateTimeOffset? OnCallConfirmationReceivedAt { get; set; }
    DateTimeOffset? OnCallConfirmationSentAt { get; set; }
    bool? OnCallIsConfirmed { get; set; }
    string OnCallRejectedReason { get; set; }     
  }
}