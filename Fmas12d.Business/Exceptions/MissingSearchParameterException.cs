using System;
using System.Runtime.Serialization;

namespace Mep.Business.Exceptions
{
  [Serializable()]
  public class MissingSearchParameterException : Exception
  {
    public MissingSearchParameterException()
      : base($"No search parameters supplied for query.")
    {
    }

    protected MissingSearchParameterException(SerializationInfo info,
                                      StreamingContext context)
      : base(info, context)
    {
    }
  }
}
