using System;
using System.Runtime.Serialization;

namespace Mep.Business.Exceptions
{
  [Serializable()]
  public class EntityCreationException : Exception
  {
    public EntityCreationException(string typeName, Exception ex)
      : base($"Failed to create entity of type {typeName}.", ex )
    {
    }

    protected EntityCreationException(SerializationInfo info,
                                      StreamingContext context)
      : base(info, context)
    {
    }
  }
}
