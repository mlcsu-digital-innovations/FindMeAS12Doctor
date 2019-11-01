using System;
using System.Runtime.Serialization;

namespace Fmas12d.Business.Exceptions
{
  [Serializable()]
  public class ExaminationAlreadyHasOutcomeException : SerilogException
  {
    public ExaminationAlreadyHasOutcomeException(
        int id,
        bool? isSuccessful,
        DateTimeOffset? completedTime,
        string userDisplayName)
    {
      PropertyValues = new object[4];
      PropertyValues[0] = id;
      PropertyValues[1] = isSuccessful.HasValue
          ? ((bool)isSuccessful ? "success" : "failure")
          : "unknown";
      PropertyValues[2] = completedTime.HasValue
          ? ((DateTimeOffset)completedTime).ToString("yyyy-MM-ddTHH:mm:sszzz")
          : "Unknown";
      PropertyValues[3] = string.IsNullOrWhiteSpace(userDisplayName)
          ? "Unknown"
          : userDisplayName;
    }

    protected ExaminationAlreadyHasOutcomeException(
      SerializationInfo info, StreamingContext context)
      : base(info, context) { }

    protected override string GetMessage()
    {
      return "Examination {0} already has its outcome set to {1} at {2} by {3}";
    }

    protected override string GetMessageTemplate()
    {
      return "Examination {ExaminationId} already has its outcome set to {IsSuccessful} " +
             "at {CompletedTime} by {UserDisplayName}";
    }
  }
}