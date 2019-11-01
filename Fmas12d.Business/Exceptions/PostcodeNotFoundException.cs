using System;
using System.Runtime.Serialization;
namespace Mep.Business.Exceptions
{
  [Serializable()]
  public class PostcodeNotFoundException : Exception
  {
    public PostcodeNotFoundException(string postcode)
      : base($"Details for examination postcode [{postcode}] not found")
    {
    }

    protected PostcodeNotFoundException(SerializationInfo info,
                                        StreamingContext context)
      : base(info, context)
    { }
  }
}