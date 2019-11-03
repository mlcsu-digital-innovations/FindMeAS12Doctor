using System;
using System.Runtime.Serialization;
namespace Fmas12d.Business.Exceptions
{
  [Serializable()]
  public class PostcodeNotFoundException : Exception
  {
    public PostcodeNotFoundException(string postcode)
      : base($"Details for assessment postcode [{postcode}] not found")
    {
    }

    protected PostcodeNotFoundException(SerializationInfo info,
                                        StreamingContext context)
      : base(info, context)
    { }
  }
}