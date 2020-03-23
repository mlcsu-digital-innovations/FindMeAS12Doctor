using System;
using System.Runtime.Serialization;
namespace Fmas12d.Business.Exceptions
{
    [Serializable()]
    public class GoogleDistanceMatrixException : Exception
    {
      public GoogleDistanceMatrixException(string status)
      : base($"Google distance matrix api error: [{status}]")
      {
      }

    protected GoogleDistanceMatrixException(SerializationInfo info,
                                        StreamingContext context)
      : base(info, context)
    { }
    }
}