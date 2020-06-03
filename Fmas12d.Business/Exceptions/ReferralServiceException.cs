using System;
using System.Runtime.Serialization;

namespace Fmas12d.Business.Exceptions
{
  [Serializable()]
  public class ReferralServiceException : Exception
  {
    public ReferralServiceException(string message)
      : base(message)
    {
    }

    protected ReferralServiceException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}