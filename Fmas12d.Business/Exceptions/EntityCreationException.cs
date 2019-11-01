using System;
using System.Runtime.Serialization;

namespace Fmas12d.Business.Exceptions
{
  [Serializable()]
  public class EntityCreationException : Exception
  {
    public EntityCreationException(string typeName, Exception ex)
      : base($"Failed to create entity of type {typeName}.", ex )
    {
    }

    public EntityCreationException(string typeName, string message)
      : base($"Failed to create entity of type {typeName}. {message}")
    {
    }

    protected EntityCreationException(SerializationInfo info,
                                      StreamingContext context)
      : base(info, context)
    {
    }
  }
}
