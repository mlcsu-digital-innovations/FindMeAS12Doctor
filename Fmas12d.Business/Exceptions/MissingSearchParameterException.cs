using System;
using System.Runtime.Serialization;

namespace Fmas12d.Business.Exceptions
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
